using FluentValidation;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Renk boş geçilemez.");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Renk minimum 2 karakter içermelidir.");
        }
    }
}
