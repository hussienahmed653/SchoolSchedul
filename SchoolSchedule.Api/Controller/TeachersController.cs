using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Teachers.Command.CreateTeacher;
using SchoolSchedule.Application.Teachers.Query.GetTeachers;

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
        [HttpGet("GetTeachers")]
        public async Task<IActionResult> GetTeachers(string? teachername = null)
        {
            var result = await _mediator.Send(new GetTeachersQuery(teachername));
            return ProblemOr(result);
        }
    }
}
