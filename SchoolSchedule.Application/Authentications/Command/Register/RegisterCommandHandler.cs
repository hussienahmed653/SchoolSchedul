using ErrorOr;
using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.Users;
using SchoolSchedule.Domain.Common.Interfaces;

namespace SchoolSchedule.Application.Authentications.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IUniteOfWork uniteOfWork,
                                      IPasswordHasher passwordHasher,
                                      IJwtTokenGenerator jwtTokenGenerator,
                                      IUserRepository userRepository)
        {
            _uniteOfWork = uniteOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand request)
        {
            try
            {
                await _uniteOfWork.BegingTransactionAsync();
                if(await _userRepository.ExistByEmailAsync(request.registerRequest.Email))
                    return Error.Conflict(description: "Email already exist");

                if(request.registerRequest.Password != request.registerRequest.ConfirmPassword)
                    return Error.Validation(description: "Password do not match");

                var hashedPassword = _passwordHasher.HashPassword(request.registerRequest.Password);

                if (hashedPassword.IsError)
                    return hashedPassword.Errors;

                var maptouser = request.registerRequest.MapToUser();

                maptouser.PasswordHash = hashedPassword.Value;

                await _userRepository.AddAsync(maptouser);

                var maptoauthresult = maptouser.MapToAuthResult();

                var token = _jwtTokenGenerator.GenerateToken(maptouser);
                maptoauthresult.Token = token;

                await _uniteOfWork.CommitAsync();
                return maptoauthresult;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: "Failed to register");
            }
        }
    }
}
