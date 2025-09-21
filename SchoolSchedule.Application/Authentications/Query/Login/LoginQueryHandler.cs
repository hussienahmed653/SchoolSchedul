using ErrorOr;
using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.Users;

namespace SchoolSchedule.Application.Authentications.Query.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IUniteOfWork uniteOfWork,
                                 IUserRepository userRepository,
                                 IJwtTokenGenerator jwtTokenGenerator)
        {
            _uniteOfWork = uniteOfWork;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthResult>> Handle(LoginQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var user = await _userRepository.GetUser(request.LoginRequest.Email);
                if (user is null)
                    return Error.NotFound(description: "User not found");
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
