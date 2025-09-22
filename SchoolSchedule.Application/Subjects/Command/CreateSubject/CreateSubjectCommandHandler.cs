using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;
using SchoolSchedule.Application.Mapping.Subjects;

namespace SchoolSchedule.Application.Subjects.Command.CreateSubject
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectCommandHandler(IUniteOfWork uniteOfWork,
                                           ISubjectRepository subjectRepository)
        {
            _uniteOfWork = uniteOfWork;
            _subjectRepository = subjectRepository;
        }

        public async Task<ErrorOr<Created>> Handle(CreateSubjectCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();

                if (await _subjectRepository.ExistByName(request.Subjectname))
                    return Error.Conflict(description: "هذه الماده موجوده من قبل");

                var subject = request.Subjectname.MapToSubject();
                await _subjectRepository.AddAsync(subject);
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
                return Error.Failure(description: "عذرا, حدثت مشكله ولا يمكن اضافة هذه الماده");
            }
        }
    }
}
