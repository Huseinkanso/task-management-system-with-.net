using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace TaskManagement.Models
{
    public class Tasks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; } = "";

        [Required]
        public string Type { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";
        [Required]
        public string Status { get; set; } = "";
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        //public Project Project { get; set; }

        [ForeignKey("Login")]
        public int LoginId { get; set; }
        public Login? Login { get; set; } 
        public Project? Project { get; set; }

        public ICollection<Comment>? Comments { get; set; }

    }
}