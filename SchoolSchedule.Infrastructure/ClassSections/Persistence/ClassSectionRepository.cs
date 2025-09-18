using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.ClassSections.Persistence
{
    internal class ClassSectionRepository : IClassSectionRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassSectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClassSection>> GetClassSectionsAsync(int? id = null)
        {
            return await _context.ClassSection
                .Where(cs => id == null || cs.GradeId == id.Value)
                .ToListAsync();
        }
    }
}
