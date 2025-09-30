using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ISchoolWeekRepository
    {
        Task<List<SchoolWeek>> GetAllAsync();
    }
}
