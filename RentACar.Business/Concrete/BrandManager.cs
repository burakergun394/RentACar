using AutoMapper;
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
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;


        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            var brandNormalizedName = brand.Name.ToUpper();
            var result = BusinessRules.Run(CheckBrandNameExist(brandNormalizedName));

            if (result != null)
                return result;

            brand.NormalizedName = brandNormalizedName;
            _brandDal.Add(brand);

            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int brandId)
        {
            var result = BusinessRules.Run(CheckBrandIdExist(brandId));

            if (result != null)
                return result;

            var deletedBrand = _brandDal.Get(b => b.Id == brandId);
            _brandDal.Delete(deletedBrand);

            return new SuccessResult(Messages.Deleted);
        }


        public IDataResult<List<Brand>> GetAll()
        {
            var result = BusinessRules.Run(CheckIfBrandCountEqualsZero());

            if (result == null)
                return new ErrorDataResult<List<Brand>>(Messages.CountEqualsZero);

            if (!result.IsSuccees)
                return result;

            return result;
        }

        public IDataResult<Brand> GetByBrandId(int brandId)
        {
            var result = BusinessRules.Run(CheckIfBrandIdExist(brandId));

            if (!result.IsSuccees)
                return result;

            return result;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            var brandNormalizedName = brand.Name.ToUpper();
            var result = BusinessRules.Run(CheckBrandNameExist(brandNormalizedName), CheckBrandIdExist(brand.Id));

            if (result != null)
                return result;

            var brandUptaded = _brandDal.Get(b => b.Id == brand.Id);

            brandUptaded.Name = brand.Name;
            brandUptaded.NormalizedName = brand.Name.ToUpper();

            _brandDal.Update(brandUptaded);

            return new SuccessResult(Messages.Uptaded);
        }

        private IDataResult<List<Brand>> CheckIfBrandCountEqualsZero()
        {
            var result = _brandDal.GetAll();

            if (result.Count == 0)
                return new ErrorDataResult<List<Brand>>(Messages.CountEqualsZero);

            return new SuccessDataResult<List<Brand>>(result);
        }

        private IDataResult<Brand> CheckIfBrandIdExist(int brandId)
        {
            var result = _brandDal.Get(b => b.Id == brandId);

            if (result == null)
                return new ErrorDataResult<Brand>(Messages.NotFound);

            return new SuccessDataResult<Brand>(result);
        }

        private IResult CheckBrandNameExist(string brandNormalizedName)
        {
            var result = _brandDal.Any(b => b.NormalizedName == brandNormalizedName);

            if (result)
                return new ErrorResult(Messages.NameAlreadyRegistered);

            return new SuccessResult();
        }

        private IResult CheckBrandIdExist(int brandId)
        {
            var result = _brandDal.Any(b => b.Id == brandId);

            if (!result)
                return new ErrorResult(Messages.NotFound);

            return new SuccessResult();
        }
    }
}