using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.ClassSections.Commands.CreateClassSection;
using SchoolSchedule.Application.ClassSections.Commands.RemoveClassSection;
using SchoolSchedule.Application.ClassSections.Querys.GetClassSections;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

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
        [HttpPost("AddClassSection")]
        public async Task<IActionResult> AddClassSection(CreateClassSectionDto createClassSection)
        {
            var result = await _mediator.Send(new CreateClassSectionCommand(createClassSection));
            return ProblemOr(result);
        }
        [HttpGet("GetClassSection")]
        public async Task<IActionResult> GetClassSection(int? id = null)
        {
            var result = await _mediator.Send(new GetClassSectionQuery(id));
            return ProblemOr(result);
        }
        [HttpDelete("RemoveClassSection")]
        public async Task<IActionResult> RemoveClassSection(CreateClassSectionDto createClassSection)
        {
            var result = await _mediator.Send(new RemoveClassSectionCommand(createClassSection));
            return ProblemOr(result);
        }
    }
}
