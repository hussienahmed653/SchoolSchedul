using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Application.ClassSections.Commands.RemoveClassSection
{
    public class RemoveClassSectionCommandHandler : IRequestHandler<RemoveClassSectionCommand, ErrorOr<Deleted>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IClassSectionRepository _classSectionRepository;

        public RemoveClassSectionCommandHandler(IUniteOfWork uniteOfWork,
                                                IClassSectionRepository classSectionRepository)
        {
            _uniteOfWork = uniteOfWork;
            _classSectionRepository = classSectionRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(RemoveClassSectionCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var classsectionexist = await _classSectionRepository.ThisClassSectionIsExist(request.ClassSectionDto);
                if (classsectionexist is null)
                    return Error.NotFound(description: "هذا الفصل غير موجود");

                await _classSectionRepository.RemoveAsync(classsectionexist);
                await _uniteOfWork.CommitAsync();
                return Result.Deleted;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "حدثت مشكله اثناء حذف هذا الفصل");
            }
        }
    }
}
