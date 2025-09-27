using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;

namespace SchoolSchedule.Application.TeacheAssignments.Command.RemoveTeacherAssignment
{
    public class RemoveTeacherAssignmentCommandHandler : IRequestHandler<RemoveTeacherAssignmentCommand, ErrorOr<Deleted>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ITeacherAssignmentRepository _assignmentRepository;
        public RemoveTeacherAssignmentCommandHandler(IUniteOfWork uniteOfWork, ITeacherAssignmentRepository assignmentRepository)
        {
            _uniteOfWork = uniteOfWork;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(RemoveTeacherAssignmentCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var teacherassignment = await _assignmentRepository.Exists(request.TeacherAssignmentDto);

                if(teacherassignment is null)
                    return Error.NotFound(description: "هذه البيانات غير موجوده");

                await _assignmentRepository.RemoveAsync(teacherassignment);

                await _uniteOfWork.CommitAsync();
                return Result.Deleted;

            }
            catch(DbUpdateException ex) when (ex.InnerException is SqlException sqlException)
            {
                await _uniteOfWork.RollbackAsync();
                var message = sqlException.MapToDbUpdateExceptionMessage();
                return Error.Conflict(description: message);
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "حدثت مشكلة اثناء الحذف");
            }
        }
    }
}
