using RentACar.Business.Abstract;
using RentACar.Business.Constants;
using RentACar.Business.ValidationRules.FluentValidation;
using RentACar.Core.Aspects.Autofac.Validation;
using RentACar.Core.Utilities.Business;
using RentACar.Core.Utilities.Results;
using RentACar.DataAccess.Abstract;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            var emailNormalizedName = user.Email.ToUpper();
            var result = BusinessRules.Run(CheckUserEmailExist(emailNormalizedName));

            if (result != null)
                return result;

            user.NormalizedEmail = emailNormalizedName;
            _userDal.Add(user);

            return new SuccessResult(Messages.Added);
        }

        public IDataResult<User> GetById(int userId)
        {
            var result = BusinessRules.Run(CheckIfUserIdExist(userId));

            if (!result.IsSuccees)
                return result;

            return result;
        }

        private IDataResult<User> CheckIfUserIdExist(int userId)
        {
            var result = _userDal.Get(u => u.Id == userId);

            if (result == null)
                return new ErrorDataResult<User>(Messages.NotFound);

            return new SuccessDataResult<User>(result);
        }

        private IResult CheckUserEmailExist(string emailNormalizedName)
        {
            var result = _userDal.Any(b => b.NormalizedEmail == emailNormalizedName);

            if (result)
                return new ErrorResult(Messages.NameAlreadyRegistered);

            return new SuccessResult();
        }
    }
}
