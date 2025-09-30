using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.SchoolWeeks.Persistence
{
    internal class SchoolWeekRepository : ISchoolWeekRepository
    {
        private readonly ApplicationDbContext _context;

        public SchoolWeekRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SchoolWeek>> GetAllAsync()
        {
            return await _context.SchoolWeeks.ToListAsync();
        }
    }
}
