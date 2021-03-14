using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Business.Abstract;
using RentACar.Business.Constants;
using RentACar.Core.Entities.Concrete;
using RentACar.Core.Utilities.Results;
using RentACar.Core.Utilities.Securities.Hashing;
using RentACar.Core.Utilities.Securities.JWT;
using RentACar.Entities.Dtos;

namespace RentACar.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePassword(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            var result =_userService.Add(user);

            if (!result.IsSuccees)
                return new ErrorDataResult<User>(result.Message);

            return new SuccessDataResult<User>(user,Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var result = _userService.GetByMail(userForLoginDto.Email);

            if (!result.IsSuccees)
                return result;

            if (!HashingHelper.VerifyPassword(userForLoginDto.Password, result.Data.PasswordHash,
                result.Data.PasswordSalt))
                return new ErrorDataResult<User>(Messages.PasswordError);

            return new SuccessDataResult<User>(result.Data, Messages.LoginSuccessful);
        }

        public IResult UserExists(string email)
        {
            var result = _userService.GetByMail(email);

            if (!result.IsSuccees)
                return new ErrorResult(Messages.UserAlreadyExist);

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.CreateAccessToken);
        }
    }
}
