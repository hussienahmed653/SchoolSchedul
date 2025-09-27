using Microsoft.Extensions.DependencyInjection;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.MediatorServices;
using System.Text.Json.Serialization;

namespace SchoolSchedule.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Application Services can be added here
            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(IRequestHandler<,>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
            //services.AddScoped<IRequest<>>();
            //services.AddScoped<IRequestHandler>();
            services.AddScoped<IMediator, Mediator>();

            services.AddControllers().AddJsonOptions(opiton =>
            {
                opiton.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            return services;
        }
    }
}
