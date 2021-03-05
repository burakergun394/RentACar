using RentACar.Business.Abstract;
using RentACar.Business.Constants;
using RentACar.Business.ValidationRules.FluentValidation;
using RentACar.Core.Aspects.Autofac.Validation;
using RentACar.Core.Utilities.Business;
using RentACar.Core.Utilities.Results;
using RentACar.DataAccess.Abstract;
using RentACar.Entities.Concrete;
using RentACar.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult();
        }

        public IResult Delete(int carId)
        {
            var result = BusinessRules.Run(CheckCarIdExist(carId));

            if (result != null)
                return result;

            var deletedCar = _carDal.Get(c => c.Id == carId);
            _carDal.Delete(deletedCar);

            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            var result = BusinessRules.Run(CheckIfCarCountEqualsZero());

            if (result == null)
                return new ErrorDataResult<List<Car>>(Messages.CountEqualsZero);

            if (!result.IsSuccees)
                return result;

            return result;
        }

        public IDataResult<Car> GetCarById(int carId)
        {
            var result = BusinessRules.Run(CheckIfCarIdExist(carId));

            if (!result.IsSuccees)
                return result;

            return result;
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            var result = BusinessRules.Run(CheckIfCarCountByBrandEqualsZero(brandId));

            if (result == null)
                return new ErrorDataResult<List<Car>>(Messages.CountEqualsZero);

            if (!result.IsSuccees)
                return result;

            return result;
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetails()
        {
            throw new NotImplementedException();
        }

        public IResult Update(Car car)
        {
            throw new NotImplementedException();
        }

        private IResult CheckCarIdExist(int carId)
        {
            var result = _carDal.Any(c => c.Id == carId);

            if (!result)
                return new ErrorResult(Messages.NotFound);

            return new SuccessResult();
        }

        private IDataResult<List<Car>> CheckIfCarCountEqualsZero()
        {
            var result = _carDal.GetAll();

            if (result.Count == 0)
                return new ErrorDataResult<List<Car>>(Messages.CountEqualsZero);

            return new SuccessDataResult<List<Car>>(result);
        }

        private IDataResult<List<Car>> CheckIfCarCountByBrandEqualsZero(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId);

            if (result.Count == 0)
                return new ErrorDataResult<List<Car>>(Messages.CountEqualsZero);

            return new SuccessDataResult<List<Car>>(result);
        }
        private IDataResult<Car> CheckIfCarIdExist(int carId)
        {
            var result = _carDal.Get(c => c.Id == carId);

            if (result == null)
                return new ErrorDataResult<Car>(Messages.NotFound);

            return new SuccessDataResult<Car>(result);
        }

    }
}
