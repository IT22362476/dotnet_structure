using Inv.Domain.Common.interfaces;

namespace Inv.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }

    }
}
