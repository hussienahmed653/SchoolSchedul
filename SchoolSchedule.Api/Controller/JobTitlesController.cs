using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.JobTitles.Query.GetJobTitle;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitlesController : BaseController
    {
        private readonly IMediator _mediator;

        public JobTitlesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetJobTitle")]
        public async Task<IActionResult> GetJobTitle()
        {
            var result = await _mediator.Send(new GetJobTitleQuery());
            return ProblemOr(result);
        }
    }
}
