using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Application.TimeTableEntries.Command.CreateTimeTableEntry
{
    public record CreateTimeTableEntryCommand : IRequest<ErrorOr<Created>>;
}
