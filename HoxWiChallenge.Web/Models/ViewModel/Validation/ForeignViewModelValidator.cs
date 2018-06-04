using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.Models.ViewModel.Validation
{
    public class ForeignViewModelValidator : AbstractValidator<ForeignViewModel>
    {
        public ForeignViewModelValidator()
        {
            //RuleFor(fvm => fvm.Id)
            //    .NotNull();

            RuleFor(fvm => fvm.FirstName)
                .NotNull();

            RuleFor(fvm => fvm.Surname)
                .NotNull();

            RuleFor(fvm => fvm.Gender)
                .NotNull();

            RuleFor(fvm => fvm.BirthdayDate)
                .NotNull();

            RuleFor(fvm => fvm.Nationality)
                .NotNull();

            RuleFor(fvm => fvm.Arrival)
                .NotNull()
                .ExclusiveBetween(new DateTime(2018, 01, 01), new DateTime(2020, 01, 01));

            RuleFor(fvm => fvm.Visa)
                .NotNull();
        }
    }
}