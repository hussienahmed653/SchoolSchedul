using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.SubjectAssignments.Command.CreateSubjectAssignment;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectAssignmentsController : BaseController
    {
        private readonly IMediator _mediator;

        public SubjectAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateSubjectAssignment")]
        public async Task<IActionResult> CreateSubjectAssignment(createSubjectAssignmentDto createSubjectAssignmentDto)
        {
            var result = await _mediator.Send(new CreateSubjectAssignmentCommand(createSubjectAssignmentDto));
            return ProblemOr(result);
        }
    }
}
