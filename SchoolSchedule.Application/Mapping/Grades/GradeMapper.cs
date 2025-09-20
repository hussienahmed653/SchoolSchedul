using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.Classes
{
    public static class GradeMapper
    {
        public static List<GradeResponseDto> MappToClassResponse(this List<Grade> classes)
        {
            return classes.Select(c => new GradeResponseDto
            {
                GradeId = c.GradeId,
                GradeYear = c.GradeYear,
                NumberOfGrades = c.NumberOfGrades,
            }).ToList();
        }
    }
}
