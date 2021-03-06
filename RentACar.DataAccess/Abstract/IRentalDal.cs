using RentACar.Core.DataAccess;
using RentACar.Entities.Concrete;
using RentACar.Entities.Dtos;
using System.Collections.Generic;

namespace RentACar.DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<RentalDetailDto> GetCarRentalDetails();

    }
}
