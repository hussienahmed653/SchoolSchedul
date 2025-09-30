using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain.Common.Interfaces;
using SchoolSchedule.Infrastructure.Authentication.PasswordHasher;
using SchoolSchedule.Infrastructure.Authentication.TokenGenerator;
using SchoolSchedule.Infrastructure.Classes.Persistence;
using SchoolSchedule.Infrastructure.ClassSections.Persistence;
using SchoolSchedule.Infrastructure.DbConext;
using SchoolSchedule.Infrastructure.Departements.Persistence;
using SchoolSchedule.Infrastructure.JopTitles.Persistence;
using SchoolSchedule.Infrastructure.Roles.Persistence;
using SchoolSchedule.Infrastructure.SchoolWeeks.Persistence;
using SchoolSchedule.Infrastructure.SubjectAssignments.Persistence;
using SchoolSchedule.Infrastructure.Subjects.Persistence;
using SchoolSchedule.Infrastructure.TeacherAssignments;
using SchoolSchedule.Infrastructure.Teachers.Persistence;
using SchoolSchedule.Infrastructure.UniteOfWork;
using SchoolSchedule.Infrastructure.UserRoles.Persistence;
using SchoolSchedule.Infrastructure.Users.Persistence;
using System.Text;

namespace SchoolSchedule.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(option =>
                                                                option.UseSqlServer(connectionString));

            services.AddScoped<IUniteOfWork, UniteOfWorkRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<ISubjectAssignmentRepository, SubjectAssignmentRepository>();
            services.AddScoped<IDepartementRepository, DepartementRepository>();
            services.AddScoped<IClassSectionRepository, ClassSectionRepository>();
            services.AddScoped<IJobTitleRepository, JobTitleRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ITeacherAssignmentRepository, TeacherAssignmentRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            services.AddScoped<ISchoolWeekRepository, SchoolWeekRepository>();

            services.AddAuthenticationDI(configuration)
                .AddAuthenticationToSwagger();
            return services;
        }

        public static IServiceCollection AddAuthenticationDI(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtsetting = new JwtSetting();
            configuration.Bind(JwtSetting.Jwt, jwtsetting);
            services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.Jwt));


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtsetting.Issuer,
                        ValidAudience = jwtsetting.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.Key)),
                    };
                });
            return services;
        }

        public static IServiceCollection AddAuthenticationToSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SchoolSchedule API",
                    Description = "API for SchoolSchedule Application"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            return services;
        }
    }
}
