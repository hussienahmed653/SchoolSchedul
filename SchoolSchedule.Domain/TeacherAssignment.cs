namespace SchoolSchedule.Domain
{
    public class TeacherAssignment
    {
        public int TeacherAssignmentId { get; set; }
        public Guid TeacherAssignmentGuid { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public int ClassSectionId { get; set; }
        public ClassSection ClassSection { get; set; }
        public ICollection<TimeTableEntry> timeTableEntries { get; set; } = new List<TimeTableEntry>();
    }
}
