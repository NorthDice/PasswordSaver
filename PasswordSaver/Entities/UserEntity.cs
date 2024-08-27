using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PasswordSaver.Entities
{
    public class UserEntity
    {
        [Key]
        [Column(TypeName = "uuid")]
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public ICollection<RoleEntity> Roles { get; set; }

        public IEnumerable<PasswordEntity> Passwords { get; set; } 
    }
}
