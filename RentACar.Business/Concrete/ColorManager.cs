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
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color color)
        {
            var normalizedName = color.Name.ToUpper();
            var result = BusinessRules.Run(CheckColorNameExist(normalizedName));

            if (result != null)
                return result;

            color.NormalizedName = normalizedName;
            _colorDal.Add(color);

            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int colorId)
        {
            var result = BusinessRules.Run(CheckColorIdExist(colorId));

            if (result != null)
                return result;

            var deletedColor = _colorDal.Get(c => c.Id == colorId);
            _colorDal.Delete(deletedColor);

            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Color>> GetAll()
        {
            var result = BusinessRules.Run(CheckIfColorCountEqualsZero());

            if (result == null)
                return new ErrorDataResult<List<Color>>(Messages.CountEqualsZero);

            if (!result.IsSuccees)
                return result;

            return result;
        }

        public IResult Update(Color color)
        {
            var normalizedName = color.Name.ToUpper();
            var result = BusinessRules.Run(CheckColorNameExist(normalizedName), CheckColorIdExist(color.Id));

            if (result != null)
                return result;

            var colorUptaded = _colorDal.Get(c => c.Id == color.Id);

            colorUptaded.Name = color.Name;
            colorUptaded.NormalizedName = color.Name.ToUpper();

            _colorDal.Update(colorUptaded);

            return new SuccessResult(Messages.Uptaded);
        }

        private IDataResult<List<Color>> CheckIfColorCountEqualsZero()
        {
            var result = _colorDal.GetAll();

            if (result.Count == 0)
                return new ErrorDataResult<List<Color>>(Messages.CountEqualsZero);

            return new SuccessDataResult<List<Color>>(result);
        }

        private IResult CheckColorNameExist(string normalizedName)
        {
            var result = _colorDal.Any(c => c.NormalizedName == normalizedName);

            if (result)
                return new ErrorResult(Messages.NameAlreadyRegistered);

            return new SuccessResult();
        }

        private IResult CheckColorIdExist(int colorId)
        {
            var result = _colorDal.Any(c => c.Id == colorId);

            if (!result)
                return new ErrorResult(Messages.NotFound);

            return new SuccessResult();
        }

    }
}
