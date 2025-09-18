using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.DTOs
{
    public class createSubjectAssignmentDto
    {
        public int SubjectId { get; set; }
        public int GradeId { get; set; }
        public int DepartementId { get; set; }
        public int ClassSectionId { get; set; }
        public Key EvenOrOdd { get; set; }
        public int Amount { get; set; }
    }
}
