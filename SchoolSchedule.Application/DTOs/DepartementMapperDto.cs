namespace SchoolSchedule.Application.DTOs
{
    public class DepartementMapperDto
    {
        public string DepartementName { get; set; }
        public List<ClassSectionDto> ClassSections { get; set; } = new();
    }
}
