using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.JobTitles.Query.GetJobTitle
{
    public record GetJobTitleQuery : IRequest<ErrorOr<List<JobTitlesResponseDto>>>;
}
