using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Teachers.Command.CreateTeacher;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : BaseController
    {
        private readonly IMediator _mediator;
        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacher(CreateTeacherDto createTeacher)
        {
            var result = await _mediator.Send(new CreateTeacherCommand(createTeacher));
            return ProblemOr(result);
        }
    }
}
