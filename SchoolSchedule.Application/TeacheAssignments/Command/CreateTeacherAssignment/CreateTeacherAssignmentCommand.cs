using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.TeacheAssignments.Command.CreateTeacherAssignment
{
    public record CreateTeacherAssignmentCommand(TeacherAssignmentDto TeacherAssignmentDto) : IRequest<ErrorOr<Created>>;
}
