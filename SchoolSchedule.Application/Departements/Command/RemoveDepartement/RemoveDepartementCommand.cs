using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Departements.Command.RemoveDepartement
{
    public record RemoveDepartementCommand(DepartementDto DepartementDto) : IRequest<ErrorOr<Deleted>>;
}
