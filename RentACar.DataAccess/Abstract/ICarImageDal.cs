﻿using RentACar.Core.DataAccess;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DataAccess.Abstract
{
    public interface ICarImageDal : IEntityRepository<CarImage>
    {
    }
}
