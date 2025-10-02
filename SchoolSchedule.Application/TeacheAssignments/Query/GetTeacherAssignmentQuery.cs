using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.TeacheAssignments.Query
{
    public record GetTeacherAssignmentQuery(int? teacherassignmentId = null) : IRequest<ErrorOr<List<TeacherAssignment>>>;
}
