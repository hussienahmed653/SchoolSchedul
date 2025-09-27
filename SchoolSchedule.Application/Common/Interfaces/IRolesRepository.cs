using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IRolesRepository
    {
        Task<List<Role>> GetAllRolesAsync();
    }
}
