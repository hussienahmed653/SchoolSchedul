using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.TeacherAssignments
{
    internal class TeacherAssignmentRepository : ITeacherAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TeacherAssignment teacherAssignment)
        {
            teacherAssignment.TeacherAssignmentId = await _context.TeacherAssignment.AnyAsync() ? await _context.TeacherAssignment.MaxAsync(t => t.TeacherAssignmentId) + 1 : 1;
            await _context.TeacherAssignment.AddAsync(teacherAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task<TeacherAssignment> Exists(TeacherAssignmentDto teacherAssignmentDto)
        {
            return await _context.TeacherAssignment
                .SingleOrDefaultAsync(t => t.TeacherId == teacherAssignmentDto.TeacherId &&
                            t.SubjectId == teacherAssignmentDto.SubjectId &&
                            t.GradeId == teacherAssignmentDto.GradeId &&
                            t.ClassSectionId == teacherAssignmentDto.ClassSectionId);
        }

        public async Task RemoveAsync(TeacherAssignment teacherAssignment)
        {
            _context.TeacherAssignment.Remove(teacherAssignment);
            await _context.SaveChangesAsync();
        }
    }
}
