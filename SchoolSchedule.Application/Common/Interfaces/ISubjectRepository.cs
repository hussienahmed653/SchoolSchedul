using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject> GetByNameAsync(string name);
        Task<bool> ExistByName(string name);
        Task AddAsync(Subject subject);
        Task RemoveAsync(Subject subject);
    }
}
