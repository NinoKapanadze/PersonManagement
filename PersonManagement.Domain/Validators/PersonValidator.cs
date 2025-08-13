using FluentValidation;
using System.Text.RegularExpressions;

namespace PersonManagement.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("First name must be at most 50 characters long.")
                .Must(BeOnlyGeorgianOrOnlyLatin)
                .WithMessage("First name must contain only Georgian letters or only Latin letters, not both.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Last name must be at most 50 characters long.")
                .Must(BeOnlyGeorgianOrOnlyLatin)
                .WithMessage("Last name must contain only Georgian letters or only Latin letters, not both.");

            RuleFor(x => x.PersonalIdNumber)
                .NotEmpty().WithMessage("Personal ID number is required.")
                .Matches(@"^\d{11}$").WithMessage("Personal ID number must be exactly 11 digits.");

            RuleFor(x => x.BirthDay)
                .Must(BeAtLeast18YearsOld)
                .WithMessage("Person must be at least 18 years old.");

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
