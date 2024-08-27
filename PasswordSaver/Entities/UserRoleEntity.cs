using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PasswordSaver.Entities
{
    public class UserRoleEntity
    {
        [Key]
        [Column(TypeName = "uuid")]
        public Guid UserId { get; set; }

        public int RoleId { get; set; }
    }
}
