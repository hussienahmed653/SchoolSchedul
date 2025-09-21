using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.SubjectAssignments;

namespace SchoolSchedule.Application.SubjectAssignments.Command.CreateSubjectAssignment
{
    public class CreateSubjectAssignmentCommandHandler : IRequestHandler<CreateSubjectAssignmentCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ISubjectAssignmentRepository _subjectAssignmentRepository;

        public CreateSubjectAssignmentCommandHandler(IUniteOfWork uniteOfWork,
                                                     ISubjectAssignmentRepository subjectAssignmentRepository)
        {
            _uniteOfWork = uniteOfWork;
            _subjectAssignmentRepository = subjectAssignmentRepository;
        }

        public async Task<ErrorOr<Created>> Handle(CreateSubjectAssignmentCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var maptosubjectassignment = request.createSubjectAssignmentrequest.MapToSubjectAssignment();
                await _subjectAssignmentRepository.AddAsync(maptosubjectassignment);
                await _uniteOfWork.CommitAsync(); 
                return Result.Created;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure("Create Subject Assignment Failure", "An error occurred while creating subject assignment.");
            }
        }
    }
}
