using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            // fetch all roles belongsTo user;
            var claims = _userService.GetRoles(user.Id);

            // CreateToken metodu user entity si ve user in rollerini ister;
            var accessToken = _tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreatedMessage);
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            // First Check if User Exists;
            var userToCheck = _userService.GetByEmail(userLoginDto.Email);
            if(userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFoundMessage);
            }

            // Check if password is correct;
            if(!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongPasswordMessage);
            }

            // Login is success;
            return new SuccessDataResult<User>(userToCheck, Messages.LoginSuccessMessage);
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);

            return new SuccessDataResult<User>(user, Messages.RegisterSuccessMessage);
        }

        public IResult UserExists(string email)
        {
            // While registering if the email already registered;
            if(_userService.GetByEmail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExistsMessage);
            }

            return new SuccessResult();
        }
    }
}
