using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher);
    }
}
