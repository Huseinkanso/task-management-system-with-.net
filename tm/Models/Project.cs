using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; } = "";

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartingDate { get; set; }

        public ICollection<Tasks>? Tasks { get; set; }
    }
}
