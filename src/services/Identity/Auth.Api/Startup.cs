using Auth.Api.Data;
using Auth.Api.Models;
using Auth.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json.Linq;

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

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PingDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                options.UseSqlite(connectionString)
                );

            services.AddScoped<IUserStore<PingUser>, SpidStore>();


            /* services.ConfigureApplicationCookie(options =>
             {
                 options.Cookie.Name = "epingcookie";
                 options.Cookie.HttpOnly = true;
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                 options.LoginPath = "/Account/Login";
                 // ReturnUrlParameter requires 
                 //using Microsoft.AspNetCore.Authentication.Cookies;
                 options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                 options.SlidingExpiration = true;
             });*/

            //services.AddIdentityServer()

            services.AddDefaultIdentity<PingUser>()
                .AddUserStore<SpidStore>()
                .AddEntityFrameworkStores<PingDbContext>()
                .AddUserManager<SpidUserManager>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
                .AddAuthentication(options=> {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.BackchannelHttpHandler=new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["auth:barear:JwtIssuer"],
                        ValidateAudience=true,
                        ValidAudience = Configuration["auth:barear:JwtIssuer"],
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["auth:barear:JwtKey"])),
                        ValidateLifetime=true,
                        ClockSkew = TimeSpan.Zero, // remove delay of token when expire,
                        
                    };
                   /* cfg.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed= ctx =>
                        {
                            ctx.Success();
                            var payload = new JObject
                            {
                                ["error"] = ctx.Exception.Message,
                               // ["error_description"] = ctx.ErrorDescription,
                                ["error_uri"] = ctx.Request.ToString()
                            };

                            return ctx.Response.WriteAsync(payload.ToString());
                        },
                        OnChallenge=  ctx =>
                        {
                            ctx.HandleResponse();
                            var payload = new JObject
                            {
                                ["error"] = ctx.Error,
                                ["error_description"] = ctx.ErrorDescription,
                                ["error_uri"] = ctx.ErrorUri
                            };

                            return  ctx.Response.WriteAsync(payload.ToString());
                        }
                        
                    };*/
                })
            ;

            services
                .AddHttpClient(Configuration["ping:name"], c => {

                    c.BaseAddress = new Uri(Configuration["ping:api:endpoint"]);
                    c.DefaultRequestHeaders.Add("Accept", "text/xml");

                })
                //.AddHttpMessageHandler<HttpMessageLogger>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());
            services
                .AddHttpClient(Configuration["auth:name"], c => {

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

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PingDbContext>();
                if (env.IsDevelopment())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

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
