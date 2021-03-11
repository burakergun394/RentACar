using System.Collections.Generic;
using RentACar.Core.Entities.Concrete;

namespace RentACar.Core.Utilities.Securities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}