using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Mapping.Classes;

namespace SchoolSchedule.Application.Clasees.Query
{
    public class GetGradeQueryHandler : IRequestHandler<GetGradeQuery, ErrorOr<List<GradeResponseDto>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IGradeRepository _classeRepository;

        public GetGradeQueryHandler(IUniteOfWork uniteOfWork,
                                     IGradeRepository classeRepository)
        {
            _uniteOfWork = uniteOfWork;
            _classeRepository = classeRepository;
        }

        public async Task<ErrorOr<List<GradeResponseDto>>> Handle(GetGradeQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                if (request.classyear is null)
                {
                    var data = await _classeRepository.GetAllAsync();
                    if (data is null)
                        return Error.NotFound(code: "Not Found", description:"Data Not Found");
                    var response = data.MappToGradeResponse();
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
