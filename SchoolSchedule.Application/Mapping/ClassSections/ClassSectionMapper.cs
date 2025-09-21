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
                GradeId = cs.GradeId,
                SectionName = cs.SectionName,
            }).ToList();
        }
        public static ClassSection MapToClassSection(this CreateClassSectionDto createClassSection)
        {
            return new ClassSection
            {
                GradeId= createClassSection.GradeId,
                SectionName = createClassSection.ClassSectionName
            };
        }
    }
}
