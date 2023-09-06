using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Details { get; set; } = "";

        
        [DataType(DataType.DateTime)]
        [Column(TypeName = "Date")]
        public DateTime? CreationDateTime { get; set; }

        [Required]
        [ForeignKey("Login")]
        public int LoginId { get; set; }

        public Login? Login { get; set; }
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Tasks? Task { get; set; }
    }
}
