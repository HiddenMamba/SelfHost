using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfHost.Infrastructure
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ReceivedNote { get; set; }


        
    }
}