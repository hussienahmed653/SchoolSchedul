using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Teachers.Command.CreateTeacher
{
    public record CreateTeacherCommand(CreateTeacherDto CreateTeacherDto) : IRequest<ErrorOr<Created>>;
}
