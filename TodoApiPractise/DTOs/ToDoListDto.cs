namespace TodoApiPractise.DTOs
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Double CompletedPercentage { get; set; }

        public bool? Done { get; set; }


    }
}
