using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Authentications.Command.AddRolesToUser;
using SchoolSchedule.Application.Authentications.Command.ChangePassword;
using SchoolSchedule.Application.Authentications.Command.Register;
using SchoolSchedule.Application.Authentications.Command.RemoveRoleFromUser;
using SchoolSchedule.Application.Authentications.Query.GetAllRoles;
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
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(ManageRoleToUserDto manageRoleToUser)
        {
            var result = await _mediator.Send(new AddRoleToUserCommand(manageRoleToUser));
            return ProblemOr(result);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            return ProblemOr(result);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(ManageRoleToUserDto manageRoleToUser)
        {
            var result = await _mediator.Send(new RemoveRoleFromUserCommand(manageRoleToUser));
            return ProblemOr(result);
        }
    }
}
