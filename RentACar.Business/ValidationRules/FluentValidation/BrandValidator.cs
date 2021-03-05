using FluentValidation;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Marka adı boş geçilemez.");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Marka ismi minimum 2 karakter içermelidir.");
        }
    }
}
