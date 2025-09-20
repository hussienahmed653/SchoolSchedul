using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Departements.Query.GetDepartement;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementsController : BaseController
    {
        private readonly IMediator _mediator;
        public DepartementsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAlldepartment")]
        public async Task<IActionResult> GetAlldepartment()
        {
            var result = await _mediator.Send(new GetDepartementQuery(null));
            return ProblemOr(result);
        }
        [HttpGet("Getdepartmentbyclassid")]
        public async Task<IActionResult> Getdepartmentbyclassid(int classid)
        {
            var result = await _mediator.Send(new GetDepartementQuery(classid));
            return ProblemOr(result);
        }
    }
}
