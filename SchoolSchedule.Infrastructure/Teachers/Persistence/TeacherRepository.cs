using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.Teachers.Persistence
{
    internal class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Teacher teacher)
        {
            teacher.TeacherId = await _context.Teachers.AnyAsync() ? _context.Teachers.Max(t => t.TeacherId) + 1 : 1;
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }
    }
}
