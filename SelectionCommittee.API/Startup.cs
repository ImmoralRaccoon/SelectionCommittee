using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SelectionCommittee.BLL.Assessments.Services;
using SelectionCommittee.BLL.Enrollees.Services;
using SelectionCommittee.BLL.Faculties.Services;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Repositories.Assessments;
using SelectionCommittee.DAL.Repositories.Enrollees;
using SelectionCommittee.DAL.Repositories.Faculties;
using SelectionCommittee.DAL.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;

namespace SelectionCommittee.API
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EBANAYAdb-PIZDEC")));
            services.AddMvc();

            services.AddTransient<IAssessmentService, AssessmentService>();
            services.AddTransient<IAssessmentRepository, AssessmentRepository>();

            services.AddTransient<IEnrolleeService, EnrolleeService>();
            services.AddTransient<IEnrolleeRepository, EnrolleeRepository>();

            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<IFacultyRepository, FacultyRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper();

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

            app.UseMvc();
        }
    }
}
