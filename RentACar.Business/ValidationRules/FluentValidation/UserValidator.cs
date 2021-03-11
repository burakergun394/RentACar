using FluentValidation;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Core.Entities.Concrete;

namespace RentACar.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Lütfen adınızı giriniz.");
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage("Adınız en az 2 karakter içermelidir.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lütfen soyadınızı giriniz.");
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage("Soyadınız en az 2 karakter içermelidir.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen emailinizi giriniz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir email giriniz.");

           
        }
    }
}
