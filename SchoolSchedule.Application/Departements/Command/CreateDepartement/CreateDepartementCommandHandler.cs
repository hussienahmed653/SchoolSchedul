using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;
using SchoolSchedule.Application.Mapping.Departements;

namespace SchoolSchedule.Application.Departements.Command.CreateDepartement
{
    public class CreateDepartementCommandHandler : IRequestHandler<CreateDepartementCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IDepartementRepository _departementRepository;

        public CreateDepartementCommandHandler(IUniteOfWork uniteOfWork,
                                               IDepartementRepository departementRepository)
        {
            _uniteOfWork = uniteOfWork;
            _departementRepository = departementRepository;
        }

        public async Task<ErrorOr<Created>> Handle(CreateDepartementCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var getdepartement = await _departementRepository.FindByGradeIdAndDepartementNameAsync(request.CreateDepartement);
                
                if (getdepartement is not null)
                    return Error.Conflict(description: "هذه الشعبه موجوده بالفعل");
                var departement = request.CreateDepartement.MapToDepartement();
                await _departementRepository.AddAsync(departement);
                await _uniteOfWork.CommitAsync();
                return Result.Created;
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
                return Error.Failure(description: "حدث خطأ أثناء اضاقة هذه الشعبه");
            }
        }
    }
}
