using Inv.Application.Interfaces.Repositories;
using Inv.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Inv.Persistence.Repositories;
using Audit.EntityFramework;
using Inv.Application.Helper;

namespace Inv.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddSingleton<DapperContext>();
            services.AddRepositories();
            services.AddHttpContextAccessor();
        }
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connectionString,
                builder =>
                {
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    /* builder.EnableRetryOnFailure(maxRetryCount: 3, // Number of retry attempts
                maxRetryDelay: TimeSpan.FromSeconds(5), // Delay between retries
                errorNumbersToAdd: null);*/

                });
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<ISqlDataAccess, SqlDataAccess>();
        }
    }
}
