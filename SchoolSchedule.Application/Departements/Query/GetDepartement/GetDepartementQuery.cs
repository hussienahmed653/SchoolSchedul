using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Departements.Query.GetDepartement
{
    public record GetDepartementQuery(int? id = null) : IRequest<ErrorOr<List<DepartementResponseDto>>>;
}
