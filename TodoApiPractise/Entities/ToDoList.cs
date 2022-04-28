using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApiPractise.Entities
{
    public class ToDoList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
       
        public DateTime StartDate { get; set; }
        

        public DateTime EndDate { get; set; }
        
        public Double CompletedPercentage { get; set; }
       
        public bool? Done { get; set; }
    }
}
