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
        
    }
}
