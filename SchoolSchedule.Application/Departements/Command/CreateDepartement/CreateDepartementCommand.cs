using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Departements.Command.CreateDepartement
{
    public record CreateDepartementCommand(DepartementDto CreateDepartement) : IRequest<ErrorOr<Created>>;
    
    
}
