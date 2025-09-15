namespace SchoolSchedule.Domain
{
    public class SubjectAssignment
    {
        public int SubjectAssignmentId { get; set; }
        public Guid SubjectAssignmentGuid { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int ClasseId { get; set; }
        public Classe Classe { get; set; }

        public int DepartementId { get; set; }
        public Departement Departement { get; set; }

        public Key EvenOrOdd { get; set; }
        public int Amount { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }

    }
    public enum Key
    {
        Even = 2,
        Odd = 1
    }
}
