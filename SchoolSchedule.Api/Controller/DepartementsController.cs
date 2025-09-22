using Microsoft.AspNetCore.Mvc;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Departements.Command.CreateDepartement;
using SchoolSchedule.Application.Departements.Command.RemoveDepartement;
using SchoolSchedule.Application.Departements.Query.GetDepartement;
using SchoolSchedule.Application.DTOs;

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
        [HttpPost("AddDepartement")]
        public async Task<IActionResult> AddDepartement(DepartementDto createDepartement)
        {
            var result = await _mediator.Send(new CreateDepartementCommand(createDepartement));
            return ProblemOr(result);
        }
        [HttpGet("GetAlldepartment")]
        public async Task<IActionResult> GetAlldepartment()
        {
            var result = await _mediator.Send(new GetDepartementQuery(null));
            return ProblemOr(result);
        }
        [HttpGet("GetdepartmentbyGrdeId")]
        public async Task<IActionResult> Getdepartmentbyclassid(int GradeId)
        {
            var result = await _mediator.Send(new GetDepartementQuery(GradeId));
            return ProblemOr(result);
        }
        [HttpDelete("RemoveDepartement")]
        public async Task<IActionResult> RemoveDepartement(DepartementDto departementDto)
        {
            var result = await _mediator.Send(new RemoveDepartementCommand(departementDto));
            return ProblemOr(result);
        }
    }
}
