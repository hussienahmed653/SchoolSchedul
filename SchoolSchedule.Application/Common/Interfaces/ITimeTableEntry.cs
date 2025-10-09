namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ITimeTableEntryRepository
    {
        Task<int> GetNextIdAsync();
    }
}
