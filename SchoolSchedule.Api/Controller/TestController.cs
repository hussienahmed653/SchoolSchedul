using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Clasees.Query;
using SchoolSchedule.Application.ClassSections.Querys.GetClassSections;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Departements.Query.GetDepartement;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.SubjectAssignments.Command.CreateSubjectAssignment;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Test")]
        public async Task<IActionResult> add(createSubjectAssignmentDto createSubjectAssignmentDto)
        {
            var result = await _mediator.Send(new CreateSubjectAssignmentCommand(createSubjectAssignmentDto));
            return ProblemOr(result);
        }
        [HttpGet("Test2")]
        public async Task<IActionResult> Getclasses()
        {
            var result = await _mediator.Send(new GetGradeQuery(null));
            return ProblemOr(result);
        }
        [HttpGet("Test3")]
        public async Task<IActionResult> Getdepartmentbyclassid(int classid)
        {
            var result = await _mediator.Send(new GetDepartementQuery(classid));
            return ProblemOr(result);
        }
        [HttpGet("Test4")]
        public async Task<IActionResult> GetAlldepartment()
        {
            var result = await _mediator.Send(new GetDepartementQuery(null));
            return ProblemOr(result);
        }
        [HttpGet("Test5")]
        public async Task<IActionResult> GetClassSection(int? id = null)
        {
            var result = await _mediator.Send(new GetClassSectionQuery(id));
            return ProblemOr(result);
        }
    }
}
