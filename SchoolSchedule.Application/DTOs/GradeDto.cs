using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.DTOs
{
    public class GradeDto
    {
        public string GradeYear { get; set; }
        public List<DepartementMapperDto> Departements { get; set; } = new();
        
    }
}
