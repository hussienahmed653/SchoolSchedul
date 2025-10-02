using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.TeacheAssignments.Query
{
    public class GetTeacherAssignmentQueryHandler : IRequestHandler<GetTeacherAssignmentQuery, ErrorOr<List<TeacherAssignment>>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ITeacherAssignmentRepository _assignmentRepository;

        public GetTeacherAssignmentQueryHandler(IUniteOfWork uniteOfWork, ITeacherAssignmentRepository assignmentRepository)
        {
            _uniteOfWork = uniteOfWork;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<ErrorOr<List<TeacherAssignment>>> Handle(GetTeacherAssignmentQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                if(request.teacherassignmentId is null)
                {
                    var allteacherassignment = await _assignmentRepository.GetAllAsync();
                    if (allteacherassignment.Count is 0)
                        return Error.NotFound(description: "لا توجد بيانات");
                    await _uniteOfWork.CommitAsync();
                    return allteacherassignment;
                }
                var teacherassignment = await _assignmentRepository.GetTeacherAssignmentByGuid(request.teacherassignmentId);
                if (teacherassignment == null)
                    return Error.NotFound(description: "لا توجد بيانات");
                await _uniteOfWork.CommitAsync();
                return new List<TeacherAssignment> { teacherassignment };
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "حدثت مشكلة اثناء جلب البيانات");
            }
        }
    }
}
