using FluentValidation;
using Microsoft.Extensions.Localization;
using PersonManagement.Shared.LocalizationResources;
using System.Text.RegularExpressions;

namespace PersonManagement.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(localizer["FirstNameRequired"])
                .MinimumLength(2).WithMessage(localizer["FirstNameMinLength"])
                .MaximumLength(50).WithMessage(localizer["FirstNameMaxLength"])
                .Must(BeOnlyGeorgianOrOnlyLatin)
                .WithMessage(localizer["FirstNameGeorgianOrLatin"]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .MinimumLength(2).WithMessage(localizer["LastNameMinLength"])
                .MaximumLength(50).WithMessage(localizer["LastNameMaxLength"])
                .Must(BeOnlyGeorgianOrOnlyLatin)
                .WithMessage(localizer["LastNameGeorgianOrLatin"]);

            RuleFor(x => x.PersonalIdNumber)
                .NotEmpty().WithMessage(localizer["PersonalIdRequired"])
                .Matches(@"^\d{11}$").WithMessage(localizer["PersonalIdFormat"]);

            RuleFor(x => x.BirthDay)
                .Must(BeAtLeast18YearsOld)
                .WithMessage(localizer["BirthDayAdult"]);

            RuleForEach(p => p.PhoneNumbers)
                .SetValidator(new PhoneNumberValidator());
        }


        private bool BeOnlyGeorgianOrOnlyLatin(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            var georgianRegex = new Regex(@"^[\u10D0-\u10FF]+$");
            var latinRegex = new Regex(@"^[a-zA-Z]+$");

            return georgianRegex.IsMatch(name) || latinRegex.IsMatch(name);
        }

        private bool BeAtLeast18YearsOld(DateOnly birthDay)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthDay.Year;
            if (birthDay > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
