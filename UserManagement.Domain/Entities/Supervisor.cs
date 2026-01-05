using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities
{
    public class Supervisor : Entity<Guid>, IAuditableEntity
    {
        public Guid UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}
