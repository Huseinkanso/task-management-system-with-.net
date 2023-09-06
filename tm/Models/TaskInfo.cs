using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace TaskManagement.Models
{
    public class TaskInfo
    {
        public IEnumerable<Project> Projects { get; set; }= new List<Project>();
        public IEnumerable<Login> Employees { get; set; }=new List<Login>();
        [Required]
        public Tasks Task { get; set; } = new();
    }
    
}
