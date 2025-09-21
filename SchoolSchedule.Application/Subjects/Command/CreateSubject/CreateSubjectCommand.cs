using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.Subjects.Command.CreateSubject
{
    public record CreateSubjectCommand(string Subjectname) : IRequest<ErrorOr<Created>>;
}
