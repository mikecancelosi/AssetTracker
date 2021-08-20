using System.ComponentModel.DataAnnotations;

namespace Quipu.Core.DomainModel
{
    public class UserPermissionOverride
    {
        [Key]
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Permission_ID { get; set; }
        public bool Allow { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
