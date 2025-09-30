namespace SchoolSchedule.Domain
{
    public class SchoolWeek
    {
        public int SchoolWeekId { get; set; }
        public Guid SchoolWeekGuid { get; set; }
        public string SchoolWeekDay { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<TimeTableEntry> timeTableEntries { get; set; } = new List<TimeTableEntry>();
    }
}
