using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
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
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "عذرا, حدثت مشكله ولا يمكن اضافة هذه الماده");
            }
        }
    }
}
