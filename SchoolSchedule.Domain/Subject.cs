namespace SchoolSchedule.Domain
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public Guid SubjectGuid { get; set; } = Guid.NewGuid();
        public string SubjectName { get; set; }
        public bool? FixedDay { get; set; } = false;
        public ICollection<SubjectAssignment> Assignments { get; set; } = new List<SubjectAssignment>();
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
    }
}
