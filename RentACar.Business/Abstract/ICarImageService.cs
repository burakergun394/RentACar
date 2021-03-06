using Microsoft.AspNetCore.Http;
using RentACar.Core.Utilities.Results;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImage carImage, IFormFile image);
        IResult Delete(int carImageId);
        IResult Update(CarImage carImage, IFormFile image);
    }
}
