using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.UserRoles.Persistence
{
    internal class UserRolesRepository : IUserRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRolesToUserAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRole> GetUserRole(string email, int roleid)
        {
            return await _context.UserRoles.
                FirstOrDefaultAsync(u => u.User.Email == email && u.RoleId == roleid);
        }

        public async Task RemoveRoleFromUserAsync(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserHasThisRole(int UserId, int RoleId)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == UserId && ur.RoleId == RoleId);
        }
    }
}
