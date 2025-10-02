using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.TeacheAssignments.Command.CreateTeacherAssignment;
using SchoolSchedule.Application.TeacheAssignments.Command.RemoveTeacherAssignment;
using SchoolSchedule.Application.TeacheAssignments.Query;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAssignmentsController : BaseController
    {
        private readonly IMediator _mediator;

        public TeacherAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateTeacherAssignment")]
        public async Task<IActionResult> CreateTeacherAssignment(TeacherAssignmentDto teacherAssignment)
        {
            var result = await _mediator.Send(new CreateTeacherAssignmentCommand(teacherAssignment));
            return ProblemOr(result);
        }
        [HttpGet("GetTeacherAssignments")]
        public async Task<IActionResult> GetTeacherAssignments(int? teacherassignmentId = null)
        {
            var result = await _mediator.Send(new GetTeacherAssignmentQuery(teacherassignmentId));
            return ProblemOr(result);
        }
        [HttpDelete("RemoveTeacherAssignment")]
        public async Task<IActionResult> RemoveTeacherAssignment(TeacherAssignmentDto teacherAssignment)
        {
            var result = await _mediator.Send(new RemoveTeacherAssignmentCommand(teacherAssignment));
            return ProblemOr(result);
        }
    }
}
