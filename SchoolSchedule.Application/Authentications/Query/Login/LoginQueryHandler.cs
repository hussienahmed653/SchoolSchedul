using ErrorOr;
using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.Users;
using SchoolSchedule.Domain.Common.Interfaces;

namespace SchoolSchedule.Application.Authentications.Query.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public LoginQueryHandler(IUniteOfWork uniteOfWork,
                                 IUserRepository userRepository,
                                 IJwtTokenGenerator jwtTokenGenerator,
                                 IPasswordHasher passwordHasher)
        {
            _uniteOfWork = uniteOfWork;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<AuthResult>> Handle(LoginQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var user = await _userRepository.GetUser(request.LoginRequest.Email);
                if (user is null)
                    return Error.Unauthorized(description: ".بيانات تسجيل دخولك لا تتطابق مع اي حساب في سجلاتنا");

                if(!_passwordHasher.VerifyPassword(request.LoginRequest.Password, user.PasswordHash))
                    return Error.Unauthorized(description: ".بيانات تسجيل دخولك لا تتطابق مع اي حساب في سجلاتنا");
                var mapusertoauthresult = user.MapToAuthResult();
                mapusertoauthresult.Token = _jwtTokenGenerator.GenerateToken(user);
                await _uniteOfWork.CommitAsync();
                return mapusertoauthresult;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure();
            }
        }
    }
}
