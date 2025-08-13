using MediatR;
using PersonManagement.Application.DTOs;

namespace PersonManagement.Application.Persons.Commands.UpdatePerson
{
    public record UpdatePersonCommand(int Id,
      string FirstName,
      string LastName,
      bool? Gender,
      string PersonalIdNumber,
      DateOnly BirthDay,
      List<PhoneNumberDto> PhoneNumbers) : IRequest<PersonDTO>;
  
}
