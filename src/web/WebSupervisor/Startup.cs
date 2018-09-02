using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using System;

namespace WebSupervisor
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.AddHealthChecks(checks =>
            {
                var minutes = 1;
                if (int.TryParse(Configuration["HealthCheck:Timeout"], out var minutesParsed))
                {
                    minutes = minutesParsed;
                }
                var isDevelopment = false;
                if (Boolean.TryParse(Configuration["IsDevelopment"], out var isDevelopmentParsed))
                    isDevelopment = isDevelopmentParsed;
                var isDockerized = false;
                if (Boolean.TryParse(Configuration["IsDockerized"], out var isDockerizedParsed))
                    isDockerized = isDockerizedParsed;

                checks.AddUrlCheckIfNotNull(Configuration.GetUrlValue("Article",isDevelopment,isDockerized), TimeSpan.FromMinutes(minutes));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            app.Map("/liveness", lapp => lapp.Run(async ctx => ctx.Response.StatusCode = 200));
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    static class ConfigurationExtensions
    {
        public static string GetUrlValue(this IConfiguration configuration,string name,bool isDevelopment,bool isDockerized)
        {
            var result = "";
            if (isDevelopment && isDockerized)
                result=configuration[name + "UrlDev"].Replace("*", configuration["DockerUrl"]);
            else if(isDevelopment)
                result=configuration[name + "UrlDev"].Replace("*","localhost");
            else
                result = configuration[name + "Url"].Replace("*", configuration["article"]);
            return result + configuration["HealthUrl"];
        }
    }
}
