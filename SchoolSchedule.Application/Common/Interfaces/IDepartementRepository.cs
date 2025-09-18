using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IDepartementRepository
    {
        Task<List<Departement>> GetByIdAsync(int classId);
        Task<List<Departement>> GetAllAsync();
    }
}
