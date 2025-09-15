namespace SchoolSchedule.Domain
{
    public class Departement
    {
        public int DepartementId { get; set; }
        public Guid DepartementGuid { get; set; } = Guid.NewGuid();
        public int ClasseId { get; set; }
        public Classe Classe { get; set; }
        public string DepartementName { get; set; }
        public ICollection<SubjectAssignment> Assignments { get; set; } = new List<SubjectAssignment>();
    }
}
