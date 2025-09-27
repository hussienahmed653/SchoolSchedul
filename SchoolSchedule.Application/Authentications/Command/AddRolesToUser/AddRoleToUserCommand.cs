using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Authentications.Command.AddRolesToUser
{
    public record AddRoleToUserCommand(ManageRoleToUserDto ManageRoleToUser) : IRequest<ErrorOr<Created>>;
}
