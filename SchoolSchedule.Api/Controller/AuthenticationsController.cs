using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Authentications.Command.ChangePassword;
using SchoolSchedule.Application.Authentications.Command.Register;
using SchoolSchedule.Application.Authentications.Query.Login;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.DTOs.Authentications;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthenticationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            var result = await _mediator.Send(new RegisterCommand(registerRequest));
            return ProblemOr(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var result = await _mediator.Send(new LoginQuery(loginRequest));
            return ProblemOr(result);
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            var result = await _mediator.Send(new ChangePasswordCommand(changePassword));
            return ProblemOr(result);
        }
    }
}
