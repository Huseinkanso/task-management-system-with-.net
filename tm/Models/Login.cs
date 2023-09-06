using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } 

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public virtual ICollection<Tasks>? Tasks { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
