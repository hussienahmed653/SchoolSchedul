using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.Classes.Persistence
{
    internal class ClassRepository : IClasseRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Classe>> GetAllAsync()
        {
            return await _context.Classes
                                .Include(c => c.Assignments)
                                .ToListAsync();
        }
    }
}
