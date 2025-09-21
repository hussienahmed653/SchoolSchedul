using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Mapping.Subjects;

namespace SchoolSchedule.Application.Subjects.Query.GetSubjects
{
    public class GetSubjectQueryHandler : IRequestHandler<GetSubjectsQuery, ErrorOr<List<SubjectsResponseDto>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ISubjectRepository _subjectRepository;

        public GetSubjectQueryHandler(IUniteOfWork uniteOfWork,
                                      ISubjectRepository subjectRepository)
        {
            _uniteOfWork = uniteOfWork;
            _subjectRepository = subjectRepository;
        }

        public async Task<ErrorOr<List<SubjectsResponseDto>>> Handle(GetSubjectsQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var subjects = await _subjectRepository.GetAllAsync();

                if (subjects.Count is 0)
                    return Error.Failure("No Subjects", "There are no subjects available");

                var mapsubjects = subjects.MapToSubjectsResponseDto();
                await _uniteOfWork.CommitAsync();
                return mapsubjects;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure("Can't Get Subjects", "Error while Getting the Subjects");
            }
        }
    }
}
