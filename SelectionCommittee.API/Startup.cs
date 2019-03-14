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
using SelectionCommittee.Email;
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
            services.AddMvc();

            services.ResolveDalDependencies(Configuration.GetConnectionString("SelectionCommitteeApi"));
            services.ResolveServicesDependencies();
            services.ResolveIdentityDependencies(Configuration.GetConnectionString("SelectionCommitteeAuthentification"));
            services.RegisterSwagger();

            services.AddAutoMapper();
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
