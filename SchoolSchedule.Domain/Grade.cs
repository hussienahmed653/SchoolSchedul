namespace SchoolSchedule.Domain
{
    public class Grade
    {
        public int GradeId { get; set; }
        public Guid GradeGuid { get; set; } = Guid.NewGuid();
        public string GradeYear { get; set; }
        public int NumberOfGrades { get; set; }
        public ICollection<Departement> Departements { get; set; } = new List<Departement>();
        public ICollection<SubjectAssignment> Assignments { get; set; } = new List<SubjectAssignment>();
        public ICollection<ClassSection> ClassSections { get; set; } = new List<ClassSection>();
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
    }
}
