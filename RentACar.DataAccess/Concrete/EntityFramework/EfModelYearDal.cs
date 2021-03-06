using RentACar.Core.DataAccess.EntityFramework;
using RentACar.DataAccess.Abstract;
using RentACar.DataAccess.Concrete.EntityFramework.Contexts;
using RentACar.Entities.Concrete;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DataAccess.Concrete.EntityFramework
{
    public class EfModelYearDal : EfEntityRepositoryBase<ModelYear, CarRentalContext>, IModelYearDal
    {
    }
}
