using System;
using System.ComponentModel.DataAnnotations;
 
namespace league.Models
{
    public class Ninja : BaseEntity
    {
        [Key]
        public long id { get; set; }
 
        [Required]
        public string Name { get; set; }
 
        [Required]
        public int Level { get; set; }
        [Required]
        public string Description { get; set; }
 
        public DateTime CreatedAt { get; set; }
 
        public DateTime UpdatedAt { get; set; }
        public int dojo_id {get; set;}
 
        public Dojo dojo { get; set; }
    }
}