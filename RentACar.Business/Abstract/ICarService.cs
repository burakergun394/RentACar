using RentACar.Core.Utilities.Results;
using RentACar.Entities.Concrete;
using RentACar.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Delete(int carId);
        IResult Update(Car car);
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetCarById(int carId);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);

        IDataResult<List<CarDetailDto>> GetCarsDetails();
    }
}
