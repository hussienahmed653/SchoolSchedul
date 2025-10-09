using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.TimeTableEntries.Command.CreateTimeTableEntry;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableEntrysController : BaseController
    {
        private readonly IMediator _mediator;

        public TimeTableEntrysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateTimeTableEntry")]
        public async Task<IActionResult> CreateTimeTableEntry()
        {
            var result = await _mediator.Send(new CreateTimeTableEntryCommand());
            return ProblemOr(result);
        }
    }
}
