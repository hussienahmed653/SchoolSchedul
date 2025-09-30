using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.SchoolWeeks.Query
{
    public record GetSchoolWeekQuery : IRequest<ErrorOr<List<SchoolWeek>>>;
}
