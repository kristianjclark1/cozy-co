using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyCo.Data.Context;
using CozyCo.Data.Implementation.SqlServer;
using CozyCo.Data.Interfaces;
using CozyCo.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CozyCo.WebUI
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

            //Add DbContext as a service
            services.AddDbContext<CozyCoDbContext>();

            //Add Identity has a service                                  //store user
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CozyCoDbContext>();

            AddServiceImplementation(services);
            AddRepositoryImplementation(services);

            //Match an interface with an Implementation
            //Wherever we have dependency in a constructor
            
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void AddRepositoryImplementation(IServiceCollection services)
        {
            services.AddSingleton<IPropertyTypeRepository, SqlServerPropertyTypeRepository>();
            services.AddSingleton<IPropertyRepository, SqlServerPropertyRepository>();
        }

        private void AddServiceImplementation(IServiceCollection services)
        {
            services.AddSingleton<IPropertyService, PropertyService>();
            services.AddSingleton<IPropertyTypeService, PropertyTypeService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
}
