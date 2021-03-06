using RentACar.Core.Utilities.Results;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IDataResult<User> GetById(int userId);
    }
}
