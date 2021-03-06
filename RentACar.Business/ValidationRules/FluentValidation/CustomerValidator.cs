using FluentValidation;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.ValidationRules.FluentValidation
{
    public class CustomerValidator: AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Lütfen şirket adınızı giriniz.");
            RuleFor(x => x.CompanyName).MinimumLength(2).WithMessage("Şirket adı en az 2 karakter içermelidir.");
        }
    }

    
}
