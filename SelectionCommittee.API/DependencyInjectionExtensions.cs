using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SelectionCommittee.API.Services.Assessments;
using SelectionCommittee.API.Services.Enrollees;
using SelectionCommittee.API.Services.Faculties;
using SelectionCommittee.Authentication;
using SelectionCommittee.Authentication.Services;
using SelectionCommittee.BLL.Assessments.Services;
using SelectionCommittee.BLL.Enrollees.Services;
using SelectionCommittee.BLL.Faculties.Services;
using SelectionCommittee.DAL.EF;
using SelectionCommittee.DAL.Repositories.FacultyEnrollees;
using SelectionCommittee.DAL.UnitOfWork;
using SelectionCommittee.Email;
using SelectionCommittee.Logger;
using Swashbuckle.AspNetCore.Swagger;

namespace SelectionCommittee.API
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection ResolveDalDependencies(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection ResolveServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAssessmentService, AssessmentService>();
            services.AddScoped<IAssessmentResponseComposer, AssessmentResponseComposer>();

            services.AddScoped<IEnrolleeService, EnrolleeService>();
            services.AddScoped<IEnrolleeResponseComposer, EnrolleeResponseComposer>();

            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IFacultyResponseComposer, FacultyResponseComposer>();

            services.AddScoped<IFacultyEnrolleeRepository, FacultyEnrolleeRepository>();

            services.AddScoped<IAuthentificationService, AuthentificationService>();
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IEmailServiceKit, EmailServiceKit>();

            return services;
        }

        public static IServiceCollection ResolveIdentityDependencies(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Selection Committee API",
                    Version = "v1"
                });
                c.IncludeXmlComments(
                    @"bin\Debug\netcoreapp2.0\SelectionCommittee.API.xml");
            });

            return services;
        }
    }
}