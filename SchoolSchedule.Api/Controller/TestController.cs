using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Clasees.Query;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.SubjectAssignments.Command.CreateSubjectAssignment;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Test")]
        public async Task<IActionResult> Get(createSubjectAssignmentDto createSubjectAssignmentDto)
        {
            var result = await _mediator.Send(new CreateSubjectAssignmentCommand(createSubjectAssignmentDto));
            return Ok(result.Value);
        }
    }
}
