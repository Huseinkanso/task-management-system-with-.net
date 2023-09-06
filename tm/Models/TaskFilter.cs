using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace tm.Models
{
    public class TaskFilter
    {
        public IEnumerable<Tasks> Tasks { get; set; }
        public IEnumerable<Login> employees { get; set; } 

        public string employee{ get; set; } 
        public string status { get; set; }
    }
}
