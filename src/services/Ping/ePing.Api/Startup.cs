using AutoMapper;
using ePing.Api.dbcontext;
using ePing.Api.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace ePing.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<EfService>();

            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IJoueurService, JoueurService>();
            services.AddTransient<PointService>();


            services.Configure<PointsSettings>(Configuration.GetSection("ping:points"));

            services
                .AddHttpClient(Configuration["ping:name"], c =>
                {

                    c.BaseAddress = new Uri(Configuration["ping:api:endpoint"]);
                    c.DefaultRequestHeaders.Add("Accept", "text/xml");

                })
                //.AddHttpMessageHandler<HttpMessageLogger>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            //  var connection = new SqliteConnection("DataSource=:memory:");
            var connection = new SqliteConnection("Data Source=ping.db");
            services.AddEntityFrameworkSqlite().AddDbContext<PingDbContext>(options =>
            {
                options.UseSqlite(connection);

            });

            services.AddAutoMapper();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            ConfigureAuthService(services);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<PingDbContext>();
                    if (env.IsDevelopment())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                    }
                }
            }




            loggerFactory.AddConsole(Configuration);


            ConfigureAuth(app);

            app.UseMvc();
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10));
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetValue<string>("IdentityUrl"); ;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "pingapi";
                });
        }

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            if (Configuration.GetValue<bool>("UseLoadTest"))
            {
                //app.UseMiddleware<ByPassAuthMiddleware>();
            }

            app.UseAuthentication();
        }
    }
}
