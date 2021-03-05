using FluentValidation;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.ValidationRules.FluentValidation
{
    public class ModelYearValidator : AbstractValidator<ModelYear>
    {
        public ModelYearValidator()
        {
            RuleFor(x => x.Year).NotEmpty().WithMessage("Yıl boş geçilemez.");
            RuleFor(x => x.Year).GreaterThan(1900).WithMessage("Girilen yıl 1900 yılından büyük olmalıdır.");
        }
    }
}
