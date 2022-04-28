using System.ComponentModel.DataAnnotations;

namespace TodoApiPractise.DTOs
{
    public abstract class ToDoListWriteDto
    {
        [Required(ErrorMessage = "You should provide a Title value.")]
        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public virtual string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual Double CompletedPercentage { get; set; }

        public virtual bool? Done { get; set; }
    }
}
