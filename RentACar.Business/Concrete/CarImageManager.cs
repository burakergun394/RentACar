using Microsoft.AspNetCore.Http;
using RentACar.Business.Abstract;
using RentACar.Business.Constants;
using RentACar.Core.Utilities.Business;
using RentACar.Core.Utilities.Helpers;
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
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        private ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        public IResult Add(CarImage carImage, IFormFile image)
        {
            var result = BusinessRules.Run(CheckCarExist(carImage.CarId), CheckCarLimitedExceed(carImage.CarId));

            if (result != null)
                return result;

            var helperResult = FileHelper.Add(image);

            if (!helperResult.IsSuccees)
                return helperResult;

            carImage.ImagePath = helperResult.Message;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int carImageId)
        {
            var result = BusinessRules.Run(CheckCarImageExistById(carImageId));

            if (result != null)
                return result;

            var deletedImage = _carImageDal.Get(c => c.Id == carImageId);

            var helperResult = FileHelper.Delete(deletedImage.ImagePath);

            if (!helperResult.IsSuccees)
                return helperResult;

            _carImageDal.Delete(deletedImage);

            return new SuccessResult(Messages.Deleted);
        }

        public IResult Update(CarImage carImage, IFormFile image)
        {
            var result = BusinessRules.Run(CheckCarImageExistById(carImage.Id), CheckCarExist(carImage.CarId), CheckCarLimitedExceed(carImage.CarId));

            if (result != null)
                return result;

            var uptadedImage = _carImageDal.Get(c => c.Id == carImage.Id);

            var helperResult = FileHelper.Update(image, uptadedImage.ImagePath);

            if (!helperResult.IsSuccees)
                return helperResult;

            uptadedImage.ImagePath = helperResult.Message;
            uptadedImage.Date = DateTime.Now;

            _carImageDal.Update(uptadedImage);

            return new SuccessResult(Messages.Uptaded);
        }

        private IResult CheckCarImageExistById(int carImageId)
        {
            var result = _carImageDal.Any(c => c.Id == carImageId);

            if (!result)
                return new ErrorResult(Messages.NotFound);

            return new SuccessResult();
        }

        private IResult CheckCarExist(int carId)
        {
            var result = _carService.GetCarById(carId);

            if (result == null)
                return new ErrorResult(Messages.NotFound);

            return new SuccessResult();
        }

        private IResult CheckCarLimitedExceed(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);

            if (result.Count >= 5)
                return new ErrorResult(Messages.CarLimited);

            return new SuccessResult();
        }
    }
}
