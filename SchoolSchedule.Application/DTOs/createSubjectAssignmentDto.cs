using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.DTOs
{
    public class createSubjectAssignmentDto
    {
        public int SubjectId { get; set; }
        public int ClasseId { get; set; }
        public int DepartementId { get; set; }
        public Key EvenOrOdd { get; set; }
        public int Amount { get; set; }
    }
}
