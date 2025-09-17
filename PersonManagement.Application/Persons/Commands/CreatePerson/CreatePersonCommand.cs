using FluentValidation.Validators;
using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public record CreatePersonCommand(
    string FirstName,
    string LastName,
    bool? Gender,
    string PersonalIdNumber,
    DateOnly BirthDay,
    List<PhoneNumberDto> PhoneNumbers,
    List<Experience> Experiences
) :
    IRequest<int>;

}
