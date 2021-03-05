using RentACar.Business.Abstract;
using RentACar.Business.Constants;
using RentACar.Core.Utilities.Business;
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
    public class ModelYearManager : IModelYearService
    {
        private IModelYearDal _modelYearDal;

        public ModelYearManager(IModelYearDal modelYearDal)
        {
            _modelYearDal = modelYearDal;
        }

        public IResult Add(ModelYear modelYear)
        {
            var result = BusinessRules.Run(CheckModelYearExist(modelYear.Year));

            if (result != null)
                return result;
     
            _modelYearDal.Add(modelYear);

            return new SuccessResult(Messages.Added);
        }

        private IResult CheckModelYearExist(int year)
        {
            var result = _modelYearDal.Any(c => c.Year == year);

            if (result)
                return new ErrorResult(Messages.YearAlreadyRegistered);

            return new SuccessResult();
        }
    }
}
