using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;

namespace SchoolSchedule.Application.Authentications.Command.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, ErrorOr<Deleted>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IUserRolesRepository _userRolesRepository;

        public RemoveRoleFromUserCommandHandler(IUniteOfWork uniteOfWork,
                                                IUserRolesRepository userRolesRepository)
        {
            _uniteOfWork = uniteOfWork;
            _userRolesRepository = userRolesRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(RemoveRoleFromUserCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var userrole = await _userRolesRepository.GetUserRole(request.ManageRoleToUser.Email, request.ManageRoleToUser.Roles);
                if (userrole is null)
                    return Error.NotFound(description: "هذا المستخدم ليس لديه هذه الصلاحيه");

                await _userRolesRepository.RemoveRoleFromUserAsync(userrole);
                await _uniteOfWork.CommitAsync();
                return Result.Deleted;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: ".حدثت مشكله اثناء حذف الصلاحيه من هذا المستخدم");
            }
        }
    }
}
