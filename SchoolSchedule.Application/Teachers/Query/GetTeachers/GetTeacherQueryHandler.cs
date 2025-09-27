using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.DTOs;
using SchoolSchedule.Application.Mapping.Teachers;
using SchoolSchedule.Domain;

namespace SchoolSchedule.Application.Teachers.Query.GetTeachers
{
    public class GetTeacherQueryHandler : IRequestHandler<GetTeachersQuery, ErrorOr<List<TeacherResponseDto>>>
    { 
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherQueryHandler(IUniteOfWork uniteOfWork,
                                      ITeacherRepository teacherRepository)
        {
            _uniteOfWork = uniteOfWork;
            _teacherRepository = teacherRepository;
        }
        public async Task<ErrorOr<List<TeacherResponseDto>>> Handle(GetTeachersQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var teachers = await _teacherRepository.GetAllAsync(request.teachername);
                if (teachers.Count is 0)
                    return Error.NotFound(description: "لا يوجد مدرسين لعرضهم");

                var teacherampping = teachers.MapToTeacherResponse();

                await _uniteOfWork.CommitAsync();
                return teacherampping;

            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "Error while Retriveing Data");
            }
        }
    }
}
