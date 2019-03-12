using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using SelectionCommittee.Authentication;
using SelectionCommittee.Authentication.Services;
using SelectionCommittee.BLL.Assessments.Services;
using SelectionCommittee.BLL.Enrollees.Services;
using SelectionCommittee.BLL.Faculties.Services;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Repositories.Assessments;
using SelectionCommittee.DAL.Repositories.Enrollees;
using SelectionCommittee.DAL.Repositories.Faculties;
using SelectionCommittee.DAL.Repositories.FacultyEnrollees;
using SelectionCommittee.DAL.UnitOfWork;
using SelectionCommittee.Logger;
using Swashbuckle.AspNetCore.Swagger;

namespace SelectionCommittee.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            loggerFactory.ConfigureNLog(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EBANAYAdb-PIZDEC")));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SelectionCommitteeAuthentification")));
            services.AddMvc();

            services.AddTransient<IAssessmentService, AssessmentService>();
            services.AddTransient<IAssessmentRepository, AssessmentRepository>();

            services.AddTransient<IEnrolleeService, EnrolleeService>();
            services.AddTransient<IEnrolleeRepository, EnrolleeRepository>();

            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<IFacultyRepository, FacultyRepository>();

            services.AddTransient<IFacultyEnrolleeRepository, FacultyEnrolleeRepository>();

            services.AddTransient<IAuthentificationService, AuthentificationService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddAutoMapper();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Selection Committee API v1",
                    Version = "v1"
                });
                //c.IncludeXmlComments(
                //    @"bin\Debug\netcoreapp2.0\EstateAgency.API.xml");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Selection Committee API v1"));

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
