using FluentValidation;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Lütfen arabanın adını giriniz.");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Araba adı en az 2 harf içermelidir.");

            RuleFor(c => c.BrandId).NotEmpty();

            RuleFor(c => c.ModelId).NotEmpty();

            RuleFor(c => c.ColorId).NotEmpty();

            RuleFor(c => c.ModelYearId).NotEmpty();

            RuleFor(c => c.DailyPrice).NotEmpty().WithMessage("Lütfen arabanın günlük fiyatını giriniz.");
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage("Arabanın günlük fiyatı 0'dan büyük olmalıdır");
        }
    }
}
