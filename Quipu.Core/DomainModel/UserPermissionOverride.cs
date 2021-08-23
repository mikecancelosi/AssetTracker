using System.ComponentModel.DataAnnotations;

namespace Quipu.Core.DomainModel
{
    public class UserPermissionOverride
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="An override needs to be set to the user. If the user does not exist then this should be deleted")]
        public int User_ID { get; set; }
        [Required(ErrorMessage ="The override needs to know what permission to override")]
        public int Permission_ID { get; set; }
        [Required(ErrorMessage ="The override need to determine whether or not the user is allowed to execute the function")]
        public bool Allow { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
