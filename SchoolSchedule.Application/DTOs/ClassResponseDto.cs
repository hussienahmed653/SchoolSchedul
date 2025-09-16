namespace SchoolSchedule.Application.DTOs
{
    public class ClassResponseDto
    {
        public int ClasseId { get; set; }
        public Guid ClasseGuid { get; set; } = Guid.NewGuid();
        public string ClasseYear { get; set; }
        public int NumberOfClasses { get; set; }
        public List<string> DepartementName { get; set; }
    }
}
