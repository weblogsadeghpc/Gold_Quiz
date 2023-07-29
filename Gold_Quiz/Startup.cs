using AutoMapper;
using Gold_Quiz.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Gold_Quiz.DataModel.Entities;
using Microsoft.AspNetCore.Identity;
using Gold_Quiz.DataModel.Services;
using Gold_Quiz.DataModel.Repository;

namespace Gold_Quiz
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
            //estefade az database service 

            //Data Base Service 
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ExamConnectionString"),
                    datamodel => datamodel.MigrationsAssembly("Gold_Quiz.DataModel"));
                //dakhel che layer hast ? Gold_Quiz.DataModel   
                //bayad connection string ra  yek ja tarif konim yeki az jaha dar appsetting.json ast
            });

            //Identity Service
            services.AddIdentity<ApplicationUsers, ApplicationRoles>(options =>
            {
                // behtar ast mahdodiat haye ramz oboor ra bardarim 
                // sakht nagir password gozashtan 
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>(); // service rahandasi shod .

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // tarif kardan mian afzarha ya middle ware ha
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            // in dota khili moheman baraye Login va bahs haye amniati va aval hatman aythentication badesh authorization

            //tarif masir
            app.UseEndpoints(endpoints =>
            {
                // map contoller haro bekhone az yek jaii
                endpoints.MapControllers();

                //default Route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");

                //Admin panel Route
                endpoints.MapAreaControllerRoute(
                    "AdminPanel",
                    "AdminPanel",
                    "AdminPanel/{Controller=AdminDashboard}/{action=Index}/{id?}"
                    );

                //Teacher panel Route
                endpoints.MapAreaControllerRoute(
                    "TeacherPanel",
                    "TeacherPanel",
                    "TeacherPanel/{Controller=TeacherDashboard}/{action=Index}/{id?}"
                    );

                //Student panel Route
                endpoints.MapAreaControllerRoute(
                    "StudentPanel",
                    "StudentPanel",
                    "StudentPanel/{Controller=StudentDashboard}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
