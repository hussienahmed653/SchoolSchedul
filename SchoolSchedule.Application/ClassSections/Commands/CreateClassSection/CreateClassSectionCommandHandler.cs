using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.ClassSections;

namespace SchoolSchedule.Application.ClassSections.Commands.CreateClassSection
{
    public class CreateClassSectionCommandHandler : IRequestHandler<CreateClassSectionCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IClassSectionRepository _classSectionRepository;

        public CreateClassSectionCommandHandler(IUniteOfWork uniteOfWork,
                                                IClassSectionRepository classSectionRepository)
        {
            _uniteOfWork = uniteOfWork;
            _classSectionRepository = classSectionRepository;
        }

        public async Task<ErrorOr<Created>> Handle(CreateClassSectionCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var classsectionexist = await _classSectionRepository.ThisClassSectionIsExist(request.CreateClassSection);
                if (classsectionexist is not null)
                    return Error.Conflict(description: "هذا الفصل موجود بالفعل");

                var classsection = request.CreateClassSection.MapToClassSection();
                await _classSectionRepository.AddAsync(classsection);
                await _uniteOfWork.CommitAsync();
                return Result.Created;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "نواجه مشكله في اضافة هذاالفصل");
            }
        }
    }
}
