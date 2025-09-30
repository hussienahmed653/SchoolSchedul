using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.SchoolWeeks.Query;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolWeeksController : BaseController
    {
        private readonly IMediator _mediator;

        public SchoolWeeksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetSchoolWeekQuery());
            return ProblemOr(result);
        }
    }
}
