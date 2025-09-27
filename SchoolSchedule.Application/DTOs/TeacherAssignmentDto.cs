namespace SchoolSchedule.Application.DTOs
{
    public class TeacherAssignmentDto
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public int GradeId { get; set; }
        public int ClassSectionId { get; set; }
    }
}
