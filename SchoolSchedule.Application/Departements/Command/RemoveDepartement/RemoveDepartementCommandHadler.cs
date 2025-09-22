using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;

namespace SchoolSchedule.Application.Departements.Command.RemoveDepartement
{
    public class RemoveDepartementCommandHadler : IRequestHandler<RemoveDepartementCommand, ErrorOr<Deleted>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IDepartementRepository _departementRepository;

        public RemoveDepartementCommandHadler(IUniteOfWork uniteOfWork,
                                              IDepartementRepository departementRepository)
        {
            _uniteOfWork = uniteOfWork;
            _departementRepository = departementRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(RemoveDepartementCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var departement = await _departementRepository.FindByGradeIdAndDepartementNameAsync(request.DepartementDto);
                if(departement is null)
                    return Error.NotFound(description: "لا يوجد شعبه بهذا الاسم");
                await _departementRepository.RemoveAsync(departement);
                await _uniteOfWork.CommitAsync();
                return Result.Deleted;
            }
            catch(DbUpdateException ex) when (ex.InnerException is SqlException sqlexc)
            {
                await _uniteOfWork.RollbackAsync();
                var message = sqlexc.MapToDbUpdateExceptionMessage();
                return Error.Conflict(description: message);
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "لا يمكن حذف هذه الشعبه");
            }
        }
    }
}
