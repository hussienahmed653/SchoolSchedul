using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.ClassSections.Querys.GetClassSections
{
    public record GetClassSectionQuery(int? gradeid = null) : IRequest<ErrorOr<List<GetClassSectionResponseDto>>>;
}
