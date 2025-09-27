using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.TeacherAssignments
{
    public static class TeacherAssignmentMapper
    {
        public static TeacherAssignment MapToTeacherAssignment(this TeacherAssignmentDto teacherAssignmentDto)
        {
            return new TeacherAssignment
            {
                TeacherId = teacherAssignmentDto.TeacherId,
                SubjectId = teacherAssignmentDto.SubjectId,
                GradeId = teacherAssignmentDto.GradeId,
                ClassSectionId = teacherAssignmentDto.ClassSectionId
            };
        }
    }
}
