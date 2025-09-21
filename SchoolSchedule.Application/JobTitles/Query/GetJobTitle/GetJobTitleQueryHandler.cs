using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Mapping.JobTitles;

namespace SchoolSchedule.Application.JobTitles.Query.GetJobTitle
{
    public class GetJobTitleQueryHandler : IRequestHandler<GetJobTitleQuery, ErrorOr<List<JobTitlesResponseDto>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IJobTitleRepository _jobTitleRepository;

        public GetJobTitleQueryHandler(IUniteOfWork uniteOfWork,
                                       IJobTitleRepository jobTitleRepository)
        {
            _uniteOfWork = uniteOfWork;
            _jobTitleRepository = jobTitleRepository;
        }

        public async Task<ErrorOr<List<JobTitlesResponseDto>>> Handle(GetJobTitleQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var jobtitles = await _jobTitleRepository.GetAllAsync();

                if(jobtitles.Count is 0)
                    return Error.Failure("No Job Titles", "There are no job titles available");

                var mapjobtitle = jobtitles.MapJobTitle();
                await _uniteOfWork.CommitAsync();
                return mapjobtitle;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure("Can't Get Job Titles", "Error while Getting the Job Titles");
            }
        }
    }
}
