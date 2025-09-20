using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IJobTitleRepository
    {
        Task<List<JobTitle>> GetAllAsync();
    }
}
