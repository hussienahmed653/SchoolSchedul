namespace SchoolSchedule.Domain
{
    public class Departement
    {
        public int DepartementId { get; set; }
        public Guid DepartementGuid { get; set; } = Guid.NewGuid();
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public string DepartementName { get; set; }
        public ICollection<SubjectAssignment> Assignments { get; set; } = new List<SubjectAssignment>();
    }
}
