using Microsoft.EntityFrameworkCore.Infrastructure;
using RentACar.Business.Abstract;
using RentACar.Business.Constants;
using RentACar.Core.Aspects.Autofac.Validation;
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
    public class ModelManager : IModelService
    {
        private IModelDal _modelDal;

        public ModelManager(IModelDal modelDal)
        {
            _modelDal = modelDal;
        }

        [ValidationAspect(typeof(ModelValidator))]
        public IResult Add(Model model)
        {
            var normalizedName = model.Name.ToUpper();
            var result = BusinessRules.Run(CheckModelNameExist(normalizedName));

            if (result != null)
                return result;

            model.NormalizedName = normalizedName;
            _modelDal.Add(model);

            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int modelId)
        {
            var result = BusinessRules.Run(CheckModelIdExist(modelId));

            if (result != null)
                return result;

            var deletedColor = _modelDal.Get(c => c.Id == modelId);
            _modelDal.Delete(deletedColor);

            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Model>> GetAll()
        {
            var result = BusinessRules.Run(CheckIfModelCountEqualsZero());

            if (result == null)
                return new ErrorDataResult<List<Model>>(Messages.CountEqualsZero);

            if (!result.IsSuccees)
                return result;

            return result;
        }

        [ValidationAspect(typeof(ModelValidator))]

        public IResult Update(Model model)
        {
            var normalizedName = model.Name.ToUpper();
            var result = BusinessRules.Run(CheckModelNameExist(normalizedName), CheckModelIdExist(model.Id));

            if (result != null)
                return result;

            var modelUptaded = _modelDal.Get(m => m.Id == model.Id);

            modelUptaded.Name = model.Name;
            modelUptaded.NormalizedName = model.Name.ToUpper();

            _modelDal.Update(modelUptaded);

            return new SuccessResult(Messages.Uptaded);
        }

        private IResult CheckModelNameExist(string normalizedName)
        {
            var result = _modelDal.Any(c => c.NormalizedName == normalizedName);

            if (result)
                return new ErrorResult(Messages.NameAlreadyRegistered);

            return new SuccessResult();
        }

        private IResult CheckModelIdExist(int modelId)
        {
            var result = _modelDal.Any(m => m.Id == modelId);

            if (!result)
                return new ErrorResult(Messages.NotFound);

            return new SuccessResult();
        }

        private IDataResult<List<Model>> CheckIfModelCountEqualsZero()
        {
            var result = _modelDal.GetAll();

            if (result.Count == 0)
                return new ErrorDataResult<List<Model>>(Messages.CountEqualsZero);

            return new SuccessDataResult<List<Model>>(result);
        }
    }
}
