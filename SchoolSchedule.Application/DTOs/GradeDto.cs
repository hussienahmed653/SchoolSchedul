using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.DTOs
{
    public class GradeDto
    {
        public string GradeYear { get; set; }
        public List<ClassSectionDto> ClassSections { get; set; } = new();
    }
}
