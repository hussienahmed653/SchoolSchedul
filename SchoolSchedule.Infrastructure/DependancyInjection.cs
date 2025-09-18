using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Infrastructure.Authentication.TokenGenerator;
using SchoolSchedule.Infrastructure.Classes.Persistence;
using SchoolSchedule.Infrastructure.DbConext;
using SchoolSchedule.Infrastructure.Departements.Persistence;
using SchoolSchedule.Infrastructure.SubjectAssignments.Persistence;
using SchoolSchedule.Infrastructure.UniteOfWork;
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
            services.AddAuthenticationDI(configuration)
                .AddAuthenticationToSwagger();
            return services;
        }

        public static IServiceCollection AddAuthenticationDI(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtsetting = new JwtSetting();
            configuration.Bind(JwtSetting.Jwt, jwtsetting);
            services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.Jwt));


            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
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
