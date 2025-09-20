using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Clasees.Query;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : BaseController
    {
        private readonly IMediator _mediator;

        public GradesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllGrades")]
        public async Task<IActionResult> GetAllGrades()
        {
            var result = await _mediator.Send(new GetGradeQuery(null));
            return ProblemOr(result);
        }
    }
}
