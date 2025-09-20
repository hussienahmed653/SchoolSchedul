namespace SchoolSchedule.Domain
{
    public class JobTitle
    {
        public int JobTitleId { get; set; }
        public Guid JobTitleGuid { get; set; }
        public string JobTitleName { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
