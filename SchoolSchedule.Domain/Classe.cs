namespace SchoolSchedule.Domain
{
    public class Classe
    {
        public int ClasseId { get; set; }
        public Guid ClasseGuid { get; set; } = Guid.NewGuid();
        public string ClasseYear { get; set; }
        public int NumberOfClasses { get; set; }
        public ICollection<Departement> Departements { get; set; } = new List<Departement>();
    }
}
