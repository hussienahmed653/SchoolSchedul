using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.Teachers;

namespace SchoolSchedule.Application.Teachers.Command.CreateTeacher
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ITeacherRepository _teacherRepository;

        public CreateTeacherCommandHandler(IUniteOfWork uniteOfWork,
                                           ITeacherRepository teacherRepository)
        {
            _uniteOfWork = uniteOfWork;
            _teacherRepository = teacherRepository;
        }

        public async Task<ErrorOr<Created>> Handle(CreateTeacherCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var data = request.CreateTeacherDto.MapToTeacher();
                await _teacherRepository.AddAsync(data);
                await _uniteOfWork.CommitAsync();
                return Result.Created;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure("Can't Add Teacher", "Error while Addeing the teacher");
            }
        }
    }
}
