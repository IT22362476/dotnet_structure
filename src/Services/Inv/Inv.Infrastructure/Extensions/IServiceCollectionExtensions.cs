using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Inv.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                // define other services here .AddTransient<IDateTimeService, DateTimeService>()
                .AddHttpContextAccessor();
        }
    }
}
