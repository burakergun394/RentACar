using RentACar.Core.Utilities.Results;
using RentACar.Entities.Concrete;
using System.Collections.Generic;

namespace RentACar.Business.Abstract
{
    public interface IModelService
    {
        IResult Add(Model model);
        IResult Delete(int modelId);
        IResult Update(Model model);
        IDataResult<List<Model>> GetAll();
    }
}
