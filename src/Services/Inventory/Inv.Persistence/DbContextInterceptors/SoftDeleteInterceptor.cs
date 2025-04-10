using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Inv.Domain.Common;
using Inv.Application.Interfaces;

namespace Inv.Persistence.DbContextInterceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(
                    eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<ISoftDeletable>> entries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<ISoftDeletable>()
                    .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDeletable> softDeletable in entries)
            {
                switch (softDeletable)
                {
                    case { State: EntityState.Deleted, Entity: BaseAuditableEntity delete  }:
                        softDeletable.State = EntityState.Modified;
                        delete.IsDeleted = true;
                        delete.ModifiedDate = DateTime.Now;
                        break;
                    case { State: EntityState.Modified, Entity: BaseAuditableEntity { IsDeleted: true} update }:
                        update.IsDeleted = false;
                        update.ModifiedDate = DateTime.Now;
                        break;

                    default:
                        softDeletable.State = EntityState.Modified;
                        softDeletable.Entity.IsDeleted = true;
                        softDeletable.Entity.DeletedOnUtc = DateTime.Now;
                        break;
                }

            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
