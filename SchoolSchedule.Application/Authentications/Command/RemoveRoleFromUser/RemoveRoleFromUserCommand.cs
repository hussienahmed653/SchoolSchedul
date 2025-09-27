using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Authentications.Command.RemoveRoleFromUser
{
    public record RemoveRoleFromUserCommand(ManageRoleToUserDto ManageRoleToUser) : IRequest<ErrorOr<Deleted>>;
    
    
}
