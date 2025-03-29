using Inv.Application.Interfaces.Repositories;
using Inv.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Inv.Persistence.DbContextInterceptors;
using Inv.Persistence.Repositories;
using Audit.EntityFramework;
using INV.Application.Interfaces.Repositories;
using Inv.Application.Helper;

namespace Inv.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
            services.AddSingleton<SoftDeleteInterceptor>();
            services.AddDbContext(configuration);
            services.AddSingleton<DapperContext>();
            services.AddRepositories();
            services.AddHttpContextAccessor();
        }
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.Configure<MessagingHubOptions>(configuration.GetSection("MessagingHub"));

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var updateAuditableInterceptor = serviceProvider.GetService<UpdateAuditableEntitiesInterceptor>();
                 options.UseSqlServer(connectionString,
                 builder => {
                     builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                     builder.EnableRetryOnFailure(maxRetryCount: 3, // Number of retry attempts
                maxRetryDelay: TimeSpan.FromSeconds(5), // Delay between retries
                errorNumbersToAdd: null);

                 })
                .AddInterceptors(updateAuditableInterceptor).AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>()).AddInterceptors(new AuditSaveChangesInterceptor());
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<ISqlDataAccess, SqlDataAccess>()
                .AddTransient(typeof(IBrandItemTypeRepository), typeof(BrandItemTypeRepository))
                .AddTransient(typeof(IRackRepository), typeof(RackRepository))
                .AddTransient(typeof(IBinLocationRepository), typeof(BinLocationRepository))
                .AddTransient(typeof(IItemRepository), typeof(ItemRepository))
                .AddTransient(typeof(ISortHelper<>), typeof(SortHelper<>))
                .AddHttpClient<IMessagingHubService, MessagingHubService>(client =>
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                });
            

        }
    }
}
