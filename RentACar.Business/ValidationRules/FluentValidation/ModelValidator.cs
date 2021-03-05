using FluentValidation;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.ValidationRules.FluentValidation
{
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Model adı boş geçilemez.");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Model ismi minimum 2 karakter içermelidir.");
        }
    }
}
