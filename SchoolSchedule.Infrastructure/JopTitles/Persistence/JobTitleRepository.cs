using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.JopTitles.Persistence
{
    internal class JobTitleRepository : IJobTitleRepository
    {
        private readonly ApplicationDbContext _context;

        public JobTitleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobTitle>> GetAllAsync()
        {
            return await _context.JobTitles.ToListAsync();
        }
    }
}
