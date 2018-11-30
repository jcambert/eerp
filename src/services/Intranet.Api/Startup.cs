using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.models;
using Intranet.Api.Seedings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using Intranet.Api.services;

namespace Intranet.Api
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
            services.AddTransient<ISeeder, Seeder>();
            services.AddTransient<ParametreService>();
            services.AddTransient(typeof(BaseService<>));
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            var c = Configuration.GetSection("api:settings");
            services.Configure<ApiSettings>(c);

            var connection = new SqliteConnection("Data Source=intranet.db");
            services.AddEntityFrameworkSqlite().AddDbContext<IntranetDbContext>(options =>
            {
                options.UseSqlite(connection);

            }, ServiceLifetime.Transient);

            services.AddAutoMapper();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();
                
                using (var serviceScope = factory.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<IntranetDbContext>();
                    var seeder = serviceScope.ServiceProvider.GetRequiredService<ISeeder>();
                   // var settings = serviceScope.ServiceProvider.GetRequiredService<IOptions<ApiSettings>>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    seeder.Seed(context);

                    context.SaveChanges();
                    
                }
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
