using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Mapping.Departements
{
    public static class DepartementsMapper
    {
        public static List<DepartementResponseDto> MapToDepartementResponse(this List<Departement> departements)
        {
            return departements.Select(d => new DepartementResponseDto
            {
                DepartementId = d.DepartementId,
                DepartementName = d.DepartementName,
            }).ToList();
        }
    }
}
