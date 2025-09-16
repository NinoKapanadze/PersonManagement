using FluentValidation;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.PersonalIdNumber)
                .NotEmpty()
                .Length(11).WithMessage("Personal ID number must be 11 digits.");

            RuleFor(x => x.PhoneNumbers)
                .NotEmpty().WithMessage("At least one phone number is required.");

        }
    }
}
