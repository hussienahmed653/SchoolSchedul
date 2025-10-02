using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.SubjectAssignments.Persistence
{
    internal class SubjectAssignmentRepository : ISubjectAssignmentRepository
    {
        private readonly ApplicationDbContext _context;
        const int pagesize = 10;

        public SubjectAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SubjectAssignment subjectAssignment)
        {
            subjectAssignment.SubjectAssignmentId = (await _context.SubjectAssignments.AnyAsync() ? (await _context.SubjectAssignments.MaxAsync(s => s.SubjectAssignmentId) + 1) : 1);
            await _context.AddAsync(subjectAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SubjectAssignment>> GetAllAsync(int pagenumber)
        {
            return await _context.SubjectAssignments
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();
        }
    }
}
