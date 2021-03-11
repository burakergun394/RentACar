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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        private IUserService _userService;

        public CustomerManager(ICustomerDal customerDal, IUserService userService)
        {
            _customerDal = customerDal;
            _userService = userService;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            var result = BusinessRules.Run(CheckUserIdExist(customer.UserId));

            if (result != null)
                return result;

            _customerDal.Add(customer);

            return new SuccessResult(Messages.Added);
        }

        private IResult CheckUserIdExist(int userId)
        {
            //var result = _userService.GetById(userId);

            //if (result == null)
            //    return new ErrorResult(result.Message);

            return new SuccessResult();
        }
    }
}
