using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.DTOs;
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

        public async Task AddAsync(ClassSection classSection)
        {
            classSection.ClassSectionId = await _context.ClassSection.AnyAsync() ? await _context.ClassSection.MaxAsync(cs => cs.ClassSectionId) + 1 : 1;
            await _context.ClassSection.AddAsync(classSection);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ClassSection>> GetClassSectionsAsync(int? id = null)
        {
            return await _context.ClassSection
                .Where(cs => id == null || cs.GradeId == id.Value)
                .ToListAsync();
        }

        public async Task RemoveAsync(ClassSection classSection)
        {
            _context.ClassSection.Remove(classSection);
            await _context.SaveChangesAsync();
        }

        public async Task<ClassSection> ThisClassSectionIsExist(CreateClassSectionDto createClassSection)
        {
            return await _context.ClassSection
                .FirstOrDefaultAsync(cs => cs.GradeId == createClassSection.GradeId && cs.SectionName == createClassSection.ClassSectionName);
        }
    }
}
