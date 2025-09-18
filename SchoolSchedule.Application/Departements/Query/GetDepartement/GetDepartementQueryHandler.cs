using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Mapping.Departements;

namespace SchoolSchedule.Application.Departements.Query.GetDepartement
{
    public class GetDepartementQueryHandler : IRequestHandler<GetDepartementQuery, ErrorOr<List<DepartementResponseDto>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IDepartementRepository _departementRepository;

        public GetDepartementQueryHandler(IUniteOfWork uniteOfWork,
                                          IDepartementRepository departementRepository)
        {
            _uniteOfWork = uniteOfWork;
            _departementRepository = departementRepository;
        }

        public async Task<ErrorOr<List<DepartementResponseDto>>> Handle(GetDepartementQuery request)
        {
            try
            {
                await _uniteOfWork.BegingTransactionAsync();
                if(request.id is not null)
                {
                    var databyid = await _departementRepository.GetByIdAsync(request.id.Value);
                    if (databyid.Count is 0)
                        return Error.NotFound(code: "Not Found", description: "Data Not Found");
                    var responsebyid = databyid.MapToDepartementResponse();
                    await _uniteOfWork.CommitAsync();
                    return responsebyid;
                }
                var data = await _departementRepository.GetAllAsync();
                if (data.Count is 0)
                    return Error.NotFound(code: "Not Found", description: "Data Not Found");
                var response = data.MapToDepartementResponse();
                await _uniteOfWork.CommitAsync();
                return response;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure("Get Departement Failure", "An error occurred while retrieving departements.");
            }
        }
    }
}
