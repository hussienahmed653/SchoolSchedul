using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IDepartementRepository
    {
        Task<List<Departement>> GetByIdAsync(int gradeid);
        Task<List<Departement>> GetAllAsync();
        Task<Departement> FindByGradeIdAndDepartementNameAsync(DepartementDto Departement);
        Task AddAsync(Departement departement);
        Task RemoveAsync(Departement departement);
    }
}
