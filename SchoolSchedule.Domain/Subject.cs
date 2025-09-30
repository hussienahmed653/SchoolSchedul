namespace SchoolSchedule.Domain
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public Guid SubjectGuid { get; set; } = Guid.NewGuid();
        public string SubjectName { get; set; }
        public bool IsFixed { get; set; } = false;
        public int? FixedDayId { get; set; }
        public SchoolWeek FixedDay { get; set; }
        public int FixedPeriod { get; set; }
        public bool IsReligious { get; set; } = false;
        public ICollection<SubjectAssignment> Assignments { get; set; } = new List<SubjectAssignment>();
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
    }
}
