using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Authentications.Command.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordDto ChangePassword) : IRequest<ErrorOr<Updated>>;
}
