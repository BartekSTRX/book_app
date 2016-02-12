using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookWebMVC.Configuration;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using BookWebMVC.Data;
using BookWebMVC.Data.Core;
using BookWebMVC.Data.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace BookWebMVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /* configure specifying settings */
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection()
                .AddEnvironmentVariables();

            var config = builder.Build();
            config["ConnectionStringDefault"] = 
                "Server=(localdb)\\MSSQLLocalDB;Database=BooksDB;Trusted_Connection=true;MultipleActiveResultSets=true";
            var paths = config["BookWebPictures"];
            config["PicturesFolderProd"] = Path.Combine(paths, "Prod");
            config["PicturesFolderSeed"] = Path.Combine(paths, "Seed");

            /* configure providing settings */
            services.AddOptions();
            services.Configure<ConnectionString>(config);
            services.Configure<PicturesFolder>(config);


            services.AddMvc(conf =>
            {
#if !DEBUG
                conf.Filters.Add(new RequireHttpsAttribute());
#endif
            });

            services.AddIdentity<BookWebUser, IdentityRole>(conf =>
            {
                // TODO configure
            }).AddEntityFrameworkStores<BookWebContext>()
            .AddDefaultTokenProviders();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BookWebContext>(/*options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]*/);

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
                conf.MapRoute("DefaultRoute", "{controller=Home}/{action=Index}/{id?}");
            });

            await dataSeeder.SeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
