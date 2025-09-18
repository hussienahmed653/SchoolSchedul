using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.ClassSections
{
    public static class ClassSectionMapper
    {
        public static List<GetClassSectionResponseDto> MappDataToGetClassSectionResponse(this List<ClassSection> classSections)
        {
            return classSections.Select(cs => new GetClassSectionResponseDto
            {
                SectionName = cs.SectionName,
            }).ToList();
        }
    }
}
