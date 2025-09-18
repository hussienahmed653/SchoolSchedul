using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Clasees.Query
{
    public record GetGradeQuery(string? classyear) : IRequest<ErrorOr<List<GradeResponseDto>>>;
}
