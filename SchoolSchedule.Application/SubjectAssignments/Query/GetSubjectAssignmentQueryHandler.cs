using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.SubjectAssignments.Query
{
    public class GetSubjectAssignmentQueryHandler : IRequestHandler<GetSubjectAssignmentQuery, ErrorOr<List<SubjectAssignment>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ISubjectAssignmentRepository _subjectAssignmentRepository;

        public GetSubjectAssignmentQueryHandler(IUniteOfWork uniteOfWork,
                                                ISubjectAssignmentRepository subjectAssignmentRepository)
        {
            _uniteOfWork = uniteOfWork;
            _subjectAssignmentRepository = subjectAssignmentRepository;
        }

        public async Task<ErrorOr<List<SubjectAssignment>>> Handle(GetSubjectAssignmentQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var subjectassignment = await _subjectAssignmentRepository.GetAllAsync(request.pagenumber);
                if(subjectassignment.Count is 0)
                    return Error.NotFound(description: "لا توجد بيانات");

                await _uniteOfWork.CommitAsync();
                return subjectassignment;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "حدثت مشكلة اثناء جلب البيانات");
            }
        }
    }
}
