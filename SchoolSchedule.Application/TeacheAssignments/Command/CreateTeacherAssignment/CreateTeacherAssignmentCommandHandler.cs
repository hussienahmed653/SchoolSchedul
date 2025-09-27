using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;
using SchoolSchedule.Application.Mapping.TeacherAssignments;

namespace SchoolSchedule.Application.TeacheAssignments.Command.CreateTeacherAssignment
{
    public class CreateTeacherAssignmentCommandHandler : IRequestHandler<CreateTeacherAssignmentCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ITeacherAssignmentRepository _teacherAssignmentRepository;

        public CreateTeacherAssignmentCommandHandler(IUniteOfWork uniteOfWork,
                                                     ITeacherAssignmentRepository teacherAssignmentRepository)
        {
            _uniteOfWork = uniteOfWork;
            _teacherAssignmentRepository = teacherAssignmentRepository;
        }

        public async Task<ErrorOr<Created>> Handle(CreateTeacherAssignmentCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var teacherassignment = await _teacherAssignmentRepository.Exists(request.TeacherAssignmentDto);

                if(teacherassignment is not null)
                    return Error.NotFound(description: "هذه البيانات موجوده من قبل");

                var teacher = request.TeacherAssignmentDto.MapToTeacherAssignment();

                await _teacherAssignmentRepository.AddAsync(teacher);
                await _uniteOfWork.CommitAsync();
                return Result.Created;
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
                return Error.Failure(description: "حدثت مشكلة اثناء الاضافه");
            }
        }
    }
}
