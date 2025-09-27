using ErrorOr;
using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Application.Authentications.Query.GetAllRoles
{
    public record GetAllRolesQuery : IRequest<ErrorOr<RolesResponse>>;
}
