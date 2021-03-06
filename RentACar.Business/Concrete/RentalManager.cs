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
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICarService carService) 
        {
            _carService = carService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var carListByCarId = GetCarByICarId(rental.CarId);

            var result = BusinessRules.Run(CheckCarExist(rental.CarId), CheckCarReturn(carListByCarId));

            if (result != null)
                return result;

            rental.RentDate = DateTime.Now;
            _rentalDal.Add(rental);

            return new SuccessResult();
        }

        public IDataResult<List<RentalDetailDto>> GetRentalCarsDetail()
        {
            var result = BusinessRules.Run(CheckIfColorCountEqualsZero());

            if (result == null)
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.CountEqualsZero);

            if (!result.IsSuccees)
                return result;

            return result;
        }

        private IResult CheckCarExist(int carId)
        {
            var result = _carService.GetCarById(carId);

            if (result == null)
                return new ErrorResult(result.Message);

            return new SuccessResult();
        }

        private IResult CheckCarReturn(List<Rental> carListByCarId)
        {
            if (carListByCarId == null)
                return new SuccessResult();

            var result = carListByCarId.Any(r => (r.ReturnDate > DateTime.Now || r.ReturnDate == null) && r.IsCarReturn == false);

            if (!result)
                return new ErrorResult(Messages.CarNotReturn);

            return new SuccessResult();
        }

        private List<Rental> GetCarByICarId(int carId)
        {
            var carListByCarId = _rentalDal.GetAll(r => r.CarId == carId);

            return carListByCarId;
        }

        private IDataResult<List<RentalDetailDto>> CheckIfColorCountEqualsZero()
        {
            var result = _rentalDal.GetCarRentalDetails();

            if (result.Count == 0)
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.CountEqualsZero);

            return new SuccessDataResult<List<RentalDetailDto>>(result);
        }


    }
}
