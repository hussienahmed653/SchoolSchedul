using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IGradeRepository
    {
        Task<List<Grade>> GetAllAsync();
    }
}
