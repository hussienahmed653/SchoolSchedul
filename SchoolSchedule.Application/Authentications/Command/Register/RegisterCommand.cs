using ErrorOr;
using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs.Authentications;

namespace SchoolSchedule.Application.Authentications.Command.Register
{
    public record RegisterCommand(RegisterRequestDto registerRequest) : IRequest<ErrorOr<AuthResult>>;
}
