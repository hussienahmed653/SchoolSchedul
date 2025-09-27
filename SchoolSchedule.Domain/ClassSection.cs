namespace SchoolSchedule.Domain
{
    public class ClassSection
    {
        public int ClassSectionId { get; set; }
        public Guid ClassSectionGuid { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public string SectionName { get; set; }

        public ICollection<SubjectAssignment> Assignments { get; set; } = new List<SubjectAssignment>();
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
    }
}
