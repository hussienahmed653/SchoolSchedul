using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<bool> UserHasThisRole(int UserId, int RoleId);
        Task AddRolesToUserAsync(UserRole userRole);
        Task<UserRole> GetUserRole(string email, int roleid);
        Task RemoveRoleFromUserAsync(UserRole userRole);
    }
}
