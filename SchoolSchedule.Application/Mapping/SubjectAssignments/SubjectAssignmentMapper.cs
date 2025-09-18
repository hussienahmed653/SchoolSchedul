using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.SubjectAssignments
{
    public static class SubjectAssignmentMapper
    {
        public static SubjectAssignment MapToSubjectAssignment(this createSubjectAssignmentDto dto)
        {
            return new Domain.SubjectAssignment
            {
                SubjectId = dto.SubjectId,
                GradeId = dto.GradeId,
                DepartementId = dto.DepartementId,
                ClassSectionId = dto.ClassSectionId,
                EvenOrOdd = dto.EvenOrOdd,
                Amount = dto.Amount,
            };
        }
    }
}
