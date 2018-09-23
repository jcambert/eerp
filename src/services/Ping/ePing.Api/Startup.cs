using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ePing.Api.dbcontext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ePing.Api.services;
using AutoMapper;

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
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IJoueurService, JoueurService>();

            services
                .AddHttpClient(Configuration["ping:name"], c => {
                    
                    c.BaseAddress = new Uri(Configuration["ping:api:endpoint"]);
                    c.DefaultRequestHeaders.Add("Accept", "text/xml");

                })
                //.AddHttpMessageHandler<HttpMessageLogger>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

         //  var connection = new SqliteConnection("DataSource=:memory:");
            var connection = new SqliteConnection("Data Source=ping.db"); 
            services.AddEntityFrameworkSqlite().AddDbContext<PingContext>(options=>
            {
                options.UseSqlite(connection);
               
            });

            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PingContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }


            loggerFactory.AddConsole(Configuration);
            app.UseMvc();
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)));
        }
    }
}
