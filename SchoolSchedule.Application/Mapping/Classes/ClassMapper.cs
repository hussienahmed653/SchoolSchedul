using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.Classes
{
    public static class ClassMapper
    {
        public static List<ClassResponseDto> MappToClassResponse(this List<Classe> classes)
        {
            return classes.Select(c => new ClassResponseDto
            {
                ClasseId = c.ClasseId,
                ClasseGuid = c.ClasseGuid,
                ClasseYear = c.ClasseYear,
                NumberOfClasses = c.NumberOfClasses,
                DepartementName = c.Departements.Select(d => d.DepartementName).ToList()
            }).ToList();
        }
    }
}
