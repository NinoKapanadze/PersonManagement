using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public record AddRelatedPersonCommand(string FirstName,
    string LastName,
    bool? Gender,
    string PersonalIdNumber,
    DateOnly BirthDay,
    List<PhoneNumberDto> PhoneNumbers,
    RelationshipType RelationshipType,
    int RelatedPersonId
 ) : IRequest<int>;
    

    
}
