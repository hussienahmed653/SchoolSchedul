using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.ClassSections.Commands.RemoveClassSection
{
    public record RemoveClassSectionCommand(CreateClassSectionDto ClassSectionDto) : IRequest<ErrorOr<Deleted>>;
}
