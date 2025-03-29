
using Inv.Domain.Common;
using Inv.Domain.Common.interfaces;
using Inv.Infrastructure.Services;
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
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddScoped<TheNumbersService>()
                // define other services here .AddTransient<IDateTimeService, DateTimeService>()
                .AddHttpContextAccessor();
        }
    }
}
