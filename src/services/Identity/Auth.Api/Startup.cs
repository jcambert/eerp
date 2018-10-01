using Auth.Api.Data;
using Auth.Api.Models;
using Auth.Api.Repositories;
using Auth.Api.Services;
using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
namespace Auth.Api
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
            services.Configure<AppSettings>(Configuration);

            services.AddScoped<SpidUserManager>();
            services.AddScoped<SpidSignInManager>();
            services.AddScoped<UserManager<PingUser>, SpidUserManager>();
            services.AddScoped<SignInManager<PingUser>, SpidSignInManager>();
            services.AddTransient<HttpClientRequest>();
            services.AddTransient<SpidRequest<PingDbContext>>();
            services.AddTransient<JoueurRepository>();


            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PingDbContext>(options =>
                options.UseSqlite(connectionString)
                );

            services.AddScoped<IUserStore<PingUser>, SpidStore>();


            services.AddDefaultIdentity<PingUser>()
               .AddUserStore<SpidStore>()
               .AddEntityFrameworkStores<PingDbContext>()
               .AddUserManager<SpidUserManager>()
               .AddDefaultTokenProviders();

            var configConnectionString = Configuration.GetConnectionString("ConfigurationConnection");
            services
                .AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlite(configConnectionString);

                    };
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients());


            services
                .AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>();


            
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
            services
                .AddHttpClient(Configuration["auth:name"], c =>
                {

                    c.BaseAddress = new Uri(Configuration["ping:api:endpoint"]);
                    c.DefaultRequestHeaders.Add("Accept", "text/xml");

                })
                //.AddHttpMessageHandler<HttpMessageLogger>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());


            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
       }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PingDbContext>();
                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                if (env.IsDevelopment())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    configContext.Database.EnsureDeleted();
                    configContext.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseAuthentication();
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
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
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
