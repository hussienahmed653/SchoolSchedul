using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Subjects.Query.GetSubjects
{
    public record GetSubjectsQuery : IRequest<ErrorOr<List<SubjectsResponseDto>>>;
}
