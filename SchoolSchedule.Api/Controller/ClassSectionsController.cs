using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.ClassSections.Querys.GetClassSections;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSectionsController : BaseController
    {
        IMediator _mediator;

        public ClassSectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("GetClassSection")]
        public async Task<IActionResult> GetClassSection(int? id = null)
        {
            var result = await _mediator.Send(new GetClassSectionQuery(id));
            return ProblemOr(result);
        }
    }
}
