using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Subjects.Command.CreateSubject;
using SchoolSchedule.Application.Subjects.Command.RemoveSubject;
using SchoolSchedule.Application.Subjects.Query.GetSubjects;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : BaseController
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddSubject")]
        public async Task<IActionResult> AddSubject(string SubjectName)
        {
            var result = await _mediator.Send(new CreateSubjectCommand(SubjectName));
            return ProblemOr(result);
        }
        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var result = await _mediator.Send(new GetSubjectsQuery());
            return ProblemOr(result);
        }
        [HttpDelete("RemoveSubject")]
        public async Task<IActionResult> RemoceSubject(string SubjectName)
        {
            var result = await _mediator.Send(new RemoveSubjectCommand(SubjectName));
            return ProblemOr(result);
        }
    }
}
