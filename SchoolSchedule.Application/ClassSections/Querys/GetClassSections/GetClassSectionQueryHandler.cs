using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Mapping.ClassSections;

namespace SchoolSchedule.Application.ClassSections.Querys.GetClassSections
{
    public class GetClassSectionQueryHandler : IRequestHandler<GetClassSectionQuery, ErrorOr<List<GetClassSectionResponseDto>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IClassSectionRepository _classSectionRepository;

        public GetClassSectionQueryHandler(IUniteOfWork uniteOfWork,
                                           IClassSectionRepository classSectionRepository)
        {
            _uniteOfWork = uniteOfWork;
            _classSectionRepository = classSectionRepository;
        }

        public async Task<ErrorOr<List<GetClassSectionResponseDto>>> Handle(GetClassSectionQuery request)
        {
            try
            {
                await _uniteOfWork.BegingTransactionAsync();
                var data = await _classSectionRepository.GetClassSectionsAsync(request.gradeid);
                if (data.Count is 0)
                    return Error.NotFound("Not Found", "Data Not Dound");
                var datamapper = data.MappDataToGetClassSectionResponse();
                return datamapper;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "Error");
            }
        }
    }
}
