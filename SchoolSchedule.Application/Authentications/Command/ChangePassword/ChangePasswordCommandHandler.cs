using ErrorOr;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Domain.Common.Interfaces;

namespace SchoolSchedule.Application.Authentications.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<Updated>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordCommandHandler(IUniteOfWork uniteOfWork,
                                            IUserRepository userRepository,
                                            ICurrentUserProvider currentUserProvider,
                                            IPasswordHasher passwordHasher)
        {
            _uniteOfWork = uniteOfWork;
            _userRepository = userRepository;
            _currentUserProvider = currentUserProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<Updated>> Handle(ChangePasswordCommand request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var currentuser = _currentUserProvider.GetCurrentUser();

                var user = await _userRepository.ExistByEmailAsync(currentuser.Email);
                if(request.ChangePassword.NewPassword != request.ChangePassword.ConfirmNewPassword)
                    return Error.Validation(description: "كلمة المرور الجديدة غير متطابقة مع تأكيد كلمة المرور");

                var hashpassword = _passwordHasher.HashPassword(request.ChangePassword.NewPassword);
                if (hashpassword.IsError)
                    return hashpassword.Errors;

                user.PasswordHash = hashpassword.Value;
                await _userRepository.ChangePasswordAsync();
                await _uniteOfWork.CommitAsync();
                return Result.Updated;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "عذرا حدث خطأ اثناء محاولة تغيير كلمة المرور الرجاء المحاوله لاحقا");
            }
        }
    }
}
