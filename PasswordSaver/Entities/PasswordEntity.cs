using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PasswordSaver.Entities
{
    public class PasswordEntity
    {
        [Key]
        [Column(TypeName = "uuid")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
    }
}
