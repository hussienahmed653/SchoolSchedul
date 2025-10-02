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

        public async Task<List<Teacher>> GetAllAsync(int pagenumber, string? teachername = null)
        {
            return await _context.Teachers
                .Where(t => (teachername == null 
                || EF.Functions.Like(t.TeacherName.ToLower(), $"%{teachername.ToLower()}%")) && t.IsActive)
                .Include(t => t.JobTitle)
                .Include(t => t.TeacherAssignments)
                .ThenInclude(ta => ta.SubjectAssignment.Subject)
                .Include(t => t.TeacherAssignments)
                .ThenInclude(ta => ta.SubjectAssignment.Grade)
                .Include(t => t.TeacherAssignments)
                .ThenInclude(ta => ta.SubjectAssignment.Departement)
                .Include(t => t.TeacherAssignments)
                .ThenInclude(ta => ta.ClassSection)
                .Skip((pagenumber - 1) * 10)
                .Take(10) 
                .ToListAsync();
        }
    }
}
