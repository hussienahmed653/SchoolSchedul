using SchoolSchedule.Api.Services;
using SchoolSchedule.Application.Common.Interfaces;

namespace SchoolSchedule.Api
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
