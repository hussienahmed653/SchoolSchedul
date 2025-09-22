using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Application.Teachers.Command.RemoveTeacher
{
    public record RemoveTeacherCommand : IRequest<ErrorOr<Deleted>>;
}
