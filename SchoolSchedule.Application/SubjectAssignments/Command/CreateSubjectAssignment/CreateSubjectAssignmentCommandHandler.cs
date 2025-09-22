using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;
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
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException)
            {
                await _uniteOfWork.RollbackAsync();
                var message = sqlException.MapToDbUpdateExceptionMessage();
                return Error.Conflict(description: message);
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure("Create Subject Assignment Failure", "An error occurred while creating subject assignment.");
            }
        }
    }
}
