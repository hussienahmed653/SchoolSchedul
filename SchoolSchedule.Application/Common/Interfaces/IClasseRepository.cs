using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IClasseRepository
    {
        Task<List<Classe>> GetAllAsync();
    }
}
