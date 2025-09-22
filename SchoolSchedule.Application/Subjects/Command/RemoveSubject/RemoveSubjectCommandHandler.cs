using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;
using SchoolSchedule.Application.Mapping.Subjects;

namespace SchoolSchedule.Application.Subjects.Command.RemoveSubject
{
    public class RemoveSubjectCommandHandler : IRequestHandler<RemoveSubjectCommand, ErrorOr<Deleted>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ISubjectRepository _subjectRepository;
        public RemoveSubjectCommandHandler(IUniteOfWork uniteOfWork,
                                           ISubjectRepository subjectRepository)
        {
            _uniteOfWork = uniteOfWork;
            _subjectRepository = subjectRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(RemoveSubjectCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var subject = await _subjectRepository.GetByNameAsync(request.SubjectName);

                if (subject is null)
                    return Error.NotFound(description: "لا يوجد ماده بهذا الاسم");

                await _subjectRepository.RemoveAsync(subject);
                await _uniteOfWork.CommitAsync();
                return Result.Deleted;
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
                return Error.Failure(description: "لا يمكن حذف هذه المادة");
            }
        }
    }
}
