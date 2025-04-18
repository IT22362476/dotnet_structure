
using Inv.Domain.Entities;
using Inv.Domain.Common;
using Inv.Domain.Common.interfaces;
using Inv.Persistence.Audit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Inv.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpcontext;

        private readonly IDomainEventDispatcher _dispatcher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
          IDomainEventDispatcher dispatcher, IHttpContextAccessor _httpcontext)
            : base(options)
        {
            _dispatcher = dispatcher;
            httpcontext = _httpcontext;
        }

        public DbSet<TheNumber> TheNumbers => Set<TheNumber>();
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<Brand> Brand => Set<Brand>();
        public DbSet<Item> Item => Set<Item>();
        public DbSet<ItemType> ItemType => Set<ItemType>();
        public DbSet<MainCategory> MainCategory => Set<MainCategory>();
        public DbSet<Model> Model => Set<Model>();
        public DbSet<SubCategory> SubCategory => Set<SubCategory>();
        public DbSet<UOM> UOM => Set<UOM>();
        public DbSet<UOMConversion> UOMConversion => Set<UOMConversion>();
        public DbSet<BrandItemType> BrandItemType => Set<BrandItemType>();
        public DbSet<BinLocation> BinLocation => Set<BinLocation>();
        public DbSet<Warehouse> Warehouse => Set<Warehouse>();
        public DbSet<Store> Store => Set<Store>();
        public DbSet<Zone> Zone => Set<Zone>();
        public DbSet<Rack> Rack => Set<Rack>();
        public DbSet<CompatibleItem> CompatibleItem => Set<CompatibleItem>();
        public DbSet<CusPriceCategory> CusPriceCategory => Set<CusPriceCategory>();
        public DbSet<Supplier> Supplier => Set<Supplier>();
        //Help for EAM remove later
        public DbSet<GRN> GRN => Set<GRN>();
        public DbSet<GRNLineItem> GRNLineItem => Set<GRNLineItem>();
        // In future work with this
        public DbSet<GRNHeader> GRNHeader => Set<GRNHeader>();
        public DbSet<GRNDetail> GRNDetail => Set<GRNDetail>();
        public DbSet<Invoice> Invoice => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItem => Set<InvoiceItem>();
        public DbSet<SystemPOHeader> SystemPOHeader => Set<SystemPOHeader>();
        public DbSet<SystemPODetail> SystemPODetail => Set<SystemPODetail>();
        // to be removed later
        public DbSet<PurchaseOrder> PurchaseOrder => Set<PurchaseOrder>();
        // to be removed later
        public DbSet<PurchaseOrderItem> PurchaseOrderItem => Set<PurchaseOrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configure default schema
            modelBuilder.HasDefaultSchema("Inv");
            modelBuilder.HasSequence<int>("BrandID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("ItemID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("ItemTypeID", schema: "dbo").StartsAt(10).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MainCategoryID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("ModelID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SubCategoryID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("UOMID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("UOMConvID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("BITID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("WHID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("StoreID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("ZoneID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("RackID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("BinLctnID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("CusPriceCatID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("CompatibleItemlID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SupplierId", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("GRNID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence<int>("GRNHeaderID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("GRNLineItemID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence<int>("GRNDetailID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("InvoiceID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("POID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("POItemID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("InvoiceItemID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("TheNumberID", schema: "dbo").StartsAt(1).IncrementsBy(1);
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int userSerialID = 0;
            var token = httpcontext?.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                userSerialID = Convert.ToInt32(jwt.Claims.First(c => c.Type == "userSerialID").Value);
            }

            foreach (var entry in ChangeTracker.Entries<AuditTrail>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Entity.UserSerialID = userSerialID;
            }
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
