using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.Roles.Persistence
{
    internal class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles
                .ToListAsync();
        }
    }
}
