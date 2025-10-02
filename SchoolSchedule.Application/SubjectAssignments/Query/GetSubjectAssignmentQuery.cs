using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.SubjectAssignments.Query
{
    public record GetSubjectAssignmentQuery(int pagenumber) : IRequest<ErrorOr<List<SubjectAssignment>>>;
}
