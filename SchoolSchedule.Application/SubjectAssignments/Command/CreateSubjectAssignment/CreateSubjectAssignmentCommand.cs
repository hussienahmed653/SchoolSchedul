using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.SubjectAssignments.Command.CreateSubjectAssignment
{
    public record CreateSubjectAssignmentCommand(createSubjectAssignmentDto createSubjectAssignmentrequest) : IRequest<ErrorOr<Created>>;
}
