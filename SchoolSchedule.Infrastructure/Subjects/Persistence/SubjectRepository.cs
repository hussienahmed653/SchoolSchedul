using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.Subjects.Persistence
{
    internal class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Subject subject)
        {
            subject.SubjectId = await _context.Subjects.AnyAsync() ? await _context.Subjects.MaxAsync(s => s.SubjectId) + 1 : 1;
            await _context.AddAsync(subject);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExistByName(string name)
        {
            return await _context.Subjects
                .AnyAsync(s => EF.Functions.Like(s.SubjectName.ToLower(), $"%{name.ToLower()}%"));
                
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Subjects
                .OrderBy(s => s.SubjectName)
                .ToListAsync();
        }

        public async Task<Subject> GetByNameAsync(string name)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectName == name);
        }

        public async Task RemoveAsync(Subject subject)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}
