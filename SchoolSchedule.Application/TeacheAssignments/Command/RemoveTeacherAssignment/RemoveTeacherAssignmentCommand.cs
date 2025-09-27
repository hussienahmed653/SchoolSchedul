using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.TeacheAssignments.Command.RemoveTeacherAssignment
{
    public record RemoveTeacherAssignmentCommand(TeacherAssignmentDto TeacherAssignmentDto) : IRequest<ErrorOr<Deleted>>;
}
