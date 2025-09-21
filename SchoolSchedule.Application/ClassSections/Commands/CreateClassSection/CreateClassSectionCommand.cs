using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;

namespace SchoolSchedule.Application.ClassSections.Commands.CreateClassSection
{
    public record CreateClassSectionCommand(CreateClassSectionDto CreateClassSection) : IRequest<ErrorOr<Created>>;
}
