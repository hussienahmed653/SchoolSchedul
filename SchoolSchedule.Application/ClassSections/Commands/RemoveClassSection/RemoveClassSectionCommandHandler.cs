using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;

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
            catch(DbUpdateException ex) when (ex.InnerException is SqlException sqlException)
            {
                await _uniteOfWork.RollbackAsync();
                var message = sqlException.MapToDbUpdateExceptionMessage();
                return Error.Conflict(description: message);
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "حدثت مشكله اثناء حذف هذا الفصل");
            }
        }
    }
}
