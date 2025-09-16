using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.SubjectAssignments.Persistence
{
    internal class SubjectAssignmentRepository : ISubjectAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SubjectAssignment subjectAssignment)
        {
            subjectAssignment.SubjectAssignmentId = (await _context.SubjectAssignments.AnyAsync() ? (await _context.SubjectAssignments.MaxAsync(s => s.SubjectAssignmentId) + 1) : 0);
            await _context.AddAsync(subjectAssignment);
            await _context.SaveChangesAsync();
        }
    }
}
