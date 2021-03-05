using RentACar.Core.Utilities.Results;
using RentACar.Entities.Concrete;
using System.Collections.Generic;

namespace RentACar.Business.Abstract
{
    public interface IModelYearService
    {
        IResult Add(ModelYear modelYear);
    }
}
