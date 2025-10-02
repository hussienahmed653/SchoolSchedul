using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.SubjectAssignments
{
    public static class SubjectAssignmentMapper
    {
        public static SubjectAssignment MapToSubjectAssignment(this createSubjectAssignmentDto dto)
        {
            return new SubjectAssignment
            {
                SubjectId = dto.SubjectId,
                GradeId = dto.GradeId,
                DepartementId = dto.DepartementId,
                EvenOrOdd = dto.EvenOrOdd,
                Amount = dto.Amount,
            };
        }
    }
}
