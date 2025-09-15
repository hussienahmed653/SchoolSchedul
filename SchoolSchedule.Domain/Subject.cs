namespace SchoolSchedule.Domain
{
    public class Subject
    {
        //public int SubjectId { get; set; }
        //public Guid SubjectGuid { get; set; } = Guid.NewGuid();
        //public string SubjectName { get; set; }
        //public int ClasseId { get; set; }
        //public ICollection<Classe> Classe { get; set; } = new List<Classe>();
        //public key EvenOrOdd { get; set; }
        //public int? DepartementId { get; set; }
        //public ICollection<Departement> Departement { get; set; } = new List<Departement>();
        //public int Amount { get; set; }
        public int SubjectId { get; set; }
        public Guid SubjectGuid { get; set; } = Guid.NewGuid();
        public string SubjectName { get; set; }
        public ICollection<SubjectAssignment> Assignments { get; set; } = new List<SubjectAssignment>();
    }
}
