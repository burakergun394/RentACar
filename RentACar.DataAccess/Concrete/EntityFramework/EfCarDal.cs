using RentACar.Core.DataAccess.EntityFramework;
using RentACar.DataAccess.Abstract;
using RentACar.DataAccess.Concrete.EntityFramework.Contexts;
using RentACar.Entities.Concrete;
using RentACar.Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join model in context.Models on car.ModelId equals model.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             join year in context.ModelYears on car.ModelYearId equals year.Id
                             select new CarDetailDto
                             {
                                 CarName = car.Name,
                                 BrandName = brand.Name,
                                 ModelName = model.Name,
                                 ColorName = color.Name,
                                 Year = year.Year,
                                 DailyPrice = car.DailyPrice
                             };

                return result.ToList();
            }
        }
    }
}
