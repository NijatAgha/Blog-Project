using Blog.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidations
{
    public class UserValidator : AbstractValidator<AppUser>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
               .NotEmpty()
               .MaximumLength(50)
               .MinimumLength(3)
               .WithName("Ad");

            RuleFor(u => u.LastName)
              .NotEmpty()
              .MaximumLength(50)
              .MinimumLength(3)
              .WithName("Soyad");

            RuleFor(u => u.PhoneNumber)
              .NotEmpty()
              .MinimumLength(10)
              .WithName("Telefon nömrəsi");

        }
    }
}
