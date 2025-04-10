using System.Reflection;
using Inv.Application.Common.Mappings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace Inv.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMediator();
            services.AddValidators();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            // Register AutoMapper with the custom profile
            services.AddAutoMapper(typeof(MappingProfile));
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }        
    }
}
