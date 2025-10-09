using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.TimeTableEntrys.Persistence
{
    internal class TimeTableEntryRepository : ITimeTableEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeTableEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextIdAsync()
        {
            return await _context.TimeTableEntries.AnyAsync() ? 
                await _context.TimeTableEntries.MaxAsync(t => t.TimeTableEntryId) + 1 : 1;
        }
    }
}
