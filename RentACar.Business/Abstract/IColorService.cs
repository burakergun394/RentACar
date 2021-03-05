using RentACar.Core.Utilities.Results;
using RentACar.Entities.Concrete;
using System.Collections.Generic;

namespace RentACar.Business.Abstract
{
    public interface IColorService
    {
        IResult Add(Color color);
        IResult Delete(int colorId);
        IResult Update(Color color);
        IDataResult<List<Color>> GetAll();
    }
}
