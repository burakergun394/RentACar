using RentACar.Core.Utilities.Results;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Core.Entities.Concrete;
using RentACar.Entities.Dtos;

namespace RentACar.Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
    }
}
