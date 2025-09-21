using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Application.Subjects.Command.RemoveSubject
{
    public record RemoveSubjectCommand(string SubjectName) : IRequest<ErrorOr<Deleted>>;
}
