using Inv.Domain.Common.interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Inv.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Inv.Persistence.DbContextInterceptors
{
    public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateAuditableEntitiesInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null)
            {
                _httpContextAccessor = httpContextAccessor;
            }
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            int userID = 0;
            var token = _httpContextAccessor?.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var applicationUserId = jwt.Claims.First(c => c.Type =="userSerialID");
                userID = applicationUserId.Value == null ? 0 : Convert.ToInt32(applicationUserId.Value);
            }

            var dbContext = eventData.Context;

            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            // track auditable changes Added / Modified / Deleted  states 
            var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Property(o => o.CreatedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.CreatedBy).CurrentValue = userID;
                        break;

                    case EntityState.Modified when entityEntry.Property(o => o.IsDeleted).CurrentValue &&
                                                   !entityEntry.Property(o => o.IsDeleted).OriginalValue:
                        entityEntry.Property(o => o.ModifiedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.ModifiedBy).CurrentValue = userID;
                        continue;

                    case EntityState.Modified:
                        entityEntry.Property(o => o.ModifiedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.ModifiedBy).CurrentValue = userID;
                        break;

                    case EntityState.Deleted:
                        entityEntry.Property(o => o.ModifiedDate).CurrentValue = DateTime.Now;
                        entityEntry.Property(o => o.ModifiedBy).CurrentValue = userID;
                        entityEntry.Property(o => o.IsDeleted).CurrentValue = true;
                        entityEntry.State = EntityState.Modified;
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    default:
                        continue;
                }
            }


            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
