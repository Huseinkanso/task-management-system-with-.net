using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace TaskManagement.Models
{
    public class CommentInfo
    {
        public int Id { get; set; }
        
        public Comment Comment { get; set; }
    }
    
}
