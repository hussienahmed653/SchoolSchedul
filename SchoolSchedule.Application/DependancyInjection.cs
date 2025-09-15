using Microsoft.Extensions.DependencyInjection;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

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
            return services;
        }
    }
}
