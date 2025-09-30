using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.SchoolWeeks.Query
{
    public class GetSchoolWeekQueryHandler : IRequestHandler<GetSchoolWeekQuery, ErrorOr<List<SchoolWeek>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ISchoolWeekRepository _schoolWeekRepository;

        public GetSchoolWeekQueryHandler(IUniteOfWork uniteOfWork,
                                         ISchoolWeekRepository schoolWeekRepository)
        {
            _uniteOfWork = uniteOfWork;
            _schoolWeekRepository = schoolWeekRepository;
        }

        public async Task<ErrorOr<List<SchoolWeek>>> Handle(GetSchoolWeekQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var getschoolweeks = await _schoolWeekRepository.GetAllAsync();
                if (getschoolweeks is null)
                    return Error.NotFound(description: "لا يوجد بيانات");
                await _uniteOfWork.CommitAsync();
                return getschoolweeks;

            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: ".حدثت مشكله أثناء استرجاع البيانات");
            }
        }
    }
}
