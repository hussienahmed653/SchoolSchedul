using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Teachers.Query.GetTeachers
{
    public class GetTeacherQueryHandler : IRequestHandler<GetTeachersQuery, ErrorOr<List<Teacher>>>
    { 
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherQueryHandler(IUniteOfWork uniteOfWork,
                                      ITeacherRepository teacherRepository)
        {
            _uniteOfWork = uniteOfWork;
            _teacherRepository = teacherRepository;
        }
        public async Task<ErrorOr<List<Teacher>>> Handle(GetTeachersQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var data = await _teacherRepository.GetAllAsync(request.teachername);
                if (data.Count is 0)
                    return Error.NotFound(code: "Not Found", description: "Data Not Found");
                await _uniteOfWork.CommitAsync();
                return data;

            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "Error while Retriveing Data");
            }
        }
    }
}
