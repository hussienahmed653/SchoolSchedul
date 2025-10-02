using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface ITeacherAssignmentRepository
    {
        Task AddAsync(TeacherAssignment teacherAssignment);
        Task<TeacherAssignment> Exists(TeacherAssignmentDto teacherAssignmentDto);
        Task RemoveAsync(TeacherAssignment teacherAssignment);
        Task<List<TeacherAssignment>> GetAllAsync();
        Task<TeacherAssignment> GetTeacherAssignmentByGuid(int? teacherassignmentId = null);
    }
}
