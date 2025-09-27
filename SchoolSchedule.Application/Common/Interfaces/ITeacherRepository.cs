using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher);
        Task<List<Teacher>> GetAllAsync(int pagenumber = 1, string? teachername = null);
    }
}
