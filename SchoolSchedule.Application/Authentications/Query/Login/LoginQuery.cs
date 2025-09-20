using ErrorOr;
using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs.Authentications;

namespace SchoolSchedule.Application.Authentications.Query.Login
{
    public record LoginQuery(LoginRequestDto LoginRequest) : IRequest<ErrorOr<AuthResult>>;
}
