using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project_Web_db.Data;
using Project_Web_db.Models;
using Project_Web_db.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Project_Web_db
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
            //using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("MySQLConnection")))
            //{
            //    connection.Open();
            //    System.Diagnostics.Debug.WriteLine(connection.ToString());
            //}

            
           services.AddTransient<EntitiesContextInitializer>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("MySQLConnection")));

          // services.AddDbContext<ApplicationDbContext>(options =>
          // options.UseSqlServer(Configuration.GetConnectionString("AzureConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



           /* services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });*/


            /* services.Configure<IdentityOptions>(options =>
             {
                 // Password settings
                 options.Password.RequireDigit = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = true;
                 options.Password.RequireLowercase = false;
                 options.Password.RequiredUniqueChars = 6;

                 // Lockout settings
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                 options.Lockout.MaxFailedAccessAttempts = 10;
                 options.Lockout.AllowedForNewUsers = true;

                 // User settings
                 options.User.RequireUniqueEmail = true;
             });*/

            /*services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });*/

            //--------------------------------------------------cookie-------------------------------
            /*services.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "CookieAuthentication",
                LoginPath = new PathString("/Account/Login"),
                AccessDeniedPath = new PathString("/Account/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });*/
 


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddDistributedMemoryCache();
            //--------------------service sesion-------------------------
            services
            .AddMvc()
            .AddSessionStateTempDataProvider();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.                        //seeder EntitiesContextInitializer personnel
        public void Configure(IApplicationBuilder app, IHostingEnvironment env ,ILoggerFactory loggerFactory, EntitiesContextInitializer personnel)
        {
            //seedder
            personnel.Seed().Wait();



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

  




            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}")
                      .MapRoute("testCreare", "{controller=Home}/{action=Index}");
            });


           

  

        }
    }
}
