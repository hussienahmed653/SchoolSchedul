using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Mapping.Classes;

namespace SchoolSchedule.Application.Clasees.Query
{
    public class GetClasseQueryHandler : IRequestHandler<GetClassesQuery, ErrorOr<List<ClassResponseDto>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IClasseRepository _classeRepository;

        public GetClasseQueryHandler(IUniteOfWork uniteOfWork,
                                     IClasseRepository classeRepository)
        {
            _uniteOfWork = uniteOfWork;
            _classeRepository = classeRepository;
        }

        public async Task<ErrorOr<List<ClassResponseDto>>> Handle(GetClassesQuery request)
        {
            try
            {
                await _uniteOfWork.BegingTransactionAsync();
                if (request.classyear is null)
                {
                    var data = await _classeRepository.GetAllAsync();
                    if (data is null)
                        return Error.NotFound(code: "Not Found", description:"Data Not Found");
                    var response = data.MappToClassResponse();
                    await _uniteOfWork.CommitAsync();
                    return response;
                }
                return Error.NotFound(code: "Not Found", description: "Data Not Found");
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure("Get Classes Failure", "An error occurred while retrieving classes.");
            }

        }
    }
}
