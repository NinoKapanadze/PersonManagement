using FluentValidation;

namespace PersonManagement.Domain.Validators
{
    public class PhoneNumberValidator : AbstractValidator<PhoneNumber>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x.PhoneType)
                .NotNull().WithMessage("Number type is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(4).WithMessage("Phone number must be at least 4 characters long.")
                .MaximumLength(50).WithMessage("Phone number must be at most 50 characters long.");
        }
    }
}
