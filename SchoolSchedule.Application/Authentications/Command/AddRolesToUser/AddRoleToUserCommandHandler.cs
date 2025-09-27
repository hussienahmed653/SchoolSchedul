using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.UserRoles;

namespace SchoolSchedule.Application.Authentications.Command.AddRolesToUser
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, ErrorOr<Created>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IUserRolesRepository _userRolesRepository;

        public AddRoleToUserCommandHandler(IUniteOfWork uniteOfWork,
                                           IUserRepository userRepository,
                                           IUserRolesRepository userRolesRepository)
        {
            _uniteOfWork = uniteOfWork;
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
        }

        public async Task<ErrorOr<Created>> Handle(AddRoleToUserCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var user = await _userRepository.ExistByEmailAsync(request.ManageRoleToUser.Email);
                if (user is null)
                    return Error.NotFound(description: ".عذرا هذا الإيميل لا يتطابق مع اي إيميل في سجلاتنا");

                if(await _userRolesRepository.UserHasThisRole(user.UserId, request.ManageRoleToUser.Roles))
                    return Error.Conflict(description: "هذا المستخدم يمتلك هذه الصلاحيه من قبل");

                var userrole = user.UserId.MappToUserRoles(request.ManageRoleToUser.Roles);
                await _userRolesRepository.AddRolesToUserAsync(userrole);
                await _uniteOfWork.CommitAsync();
                return Result.Created;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "عذرا حدث خطأ اثناء الإضافه الرجاء المحاوله لاحقا");
            }
        }
    }
}
