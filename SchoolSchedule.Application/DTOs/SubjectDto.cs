using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.DTOs
{
    public class SubjectDto
    {
        public string SubjectName { get; set; }
        public List<GradeDto> Grade { get; set; } = new();
    }
}
