using RentACar.Core.DataAccess.EntityFramework;
using RentACar.DataAccess.Abstract;
using RentACar.DataAccess.Concrete.EntityFramework.Contexts;
using RentACar.Entities.Concrete;
using RentACar.Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetCarRentalDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result = from rental in context.Rentals
                             join customer in context.Customers on rental.CustomerId equals customer.Id
                             join user in context.Users on customer.UserId equals user.Id
                             join car in context.Cars on rental.CarId equals car.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join model in context.Models on car.ModelId equals model.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             join year in context.ModelYears on car.ModelYearId equals year.Id
                             select new RentalDetailDto
                             {
                                 RentalId = rental.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 CarName = car.Name,
                                 BrandName = brand.Name,
                                 ModelName = model.Name,
                                 ColorName = color.Name,
                                 ModelYear = year.Year,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };

                return result.ToList();
            }
        }
    }
}
