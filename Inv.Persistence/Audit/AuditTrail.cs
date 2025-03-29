using Audit.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Inv.Persistence.Audit
{
    [AuditIgnore]
    public class AuditTrail
    {
        [Key]
        public long AudtTralID { get; set; }
        public DateTime AuditDateTimeUtc { get; set; }
        [Required]
        public int? UserSerialID { get; set; }
        [Required]
        public int? FrmSerialID { get; set; }
        [Required]
        public long? LoginLogSerialID { get; set; }
        [Required]
        public string? TableName { get; set; }
        [Required]
        public string? AuditData { get; set; }
        [Required]
        public string? MachineName { get; set; }
        [Required]
        public string? Action { get; set; }


    }
}
