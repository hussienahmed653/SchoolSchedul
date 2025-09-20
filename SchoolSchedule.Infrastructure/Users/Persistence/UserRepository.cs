using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.Users.Persistence
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            user.UserId = await _context.Users.AnyAsync() ? await _context.Users.MaxAsync(u => u.UserId) + 1 : 1;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(u => u.Role)
                .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
