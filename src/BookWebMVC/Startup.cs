using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using BookWebMVC.Data;
using BookWebMVC.Data.Core;
using BookWebMVC.Data.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookWebMVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(conf =>
            {
#if !DEBUG
                conf.Filters.Add(new RequireHttpsAttribute());
#endif
            });

            services.AddIdentity<BookWebUser, IdentityRole>(conf =>
            {
                // TODO configure
            }).AddEntityFrameworkStores<BookWebContext>();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BookWebContext>();

            services.AddScoped<BookWebContext>();
            services.AddTransient<DataSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, DataSeeder dataSeeder)
        {
            app.UseIISPlatformHandler();
            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(conf =>
            {
                conf.MapRoute("DefaultRoute", "{controller}/{action}/{id?}", defaults: new
                {
                    controller = "Home",
                    action = "Index"
                });
            });

            await dataSeeder.SeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
