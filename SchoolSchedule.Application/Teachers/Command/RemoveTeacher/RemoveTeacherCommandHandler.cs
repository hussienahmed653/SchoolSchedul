using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.DBUpdateExceptions;

namespace SchoolSchedule.Application.Teachers.Command.RemoveTeacher
{
    public class RemoveTeacherCommandHandler : IRequestHandler<RemoveTeacherCommand, ErrorOr<Deleted>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ITeacherRepository _teacherRepository;

        public RemoveTeacherCommandHandler(IUniteOfWork uniteOfWork,
                                           ITeacherRepository teacherRepository)
        {
            _uniteOfWork = uniteOfWork;
            _teacherRepository = teacherRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(RemoveTeacherCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                // هنستني علي حتة حذف المدرس دي شويه لاني هضيف حاجات كتير هتاخد ال Id بتاع المدرس فحتاج وانا بحذف اعمل تشيك علي كل دول
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
                return Error.Failure(description: "حدث خطأ اثناء حذف هذا المدرس");
            }
        }
    }
}
