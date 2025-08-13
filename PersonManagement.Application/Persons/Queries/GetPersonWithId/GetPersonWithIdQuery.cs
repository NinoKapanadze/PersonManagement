using MediatR;
using PersonManagement.Application.DTOs;

namespace PersonManagement.Application.Persons.Queries.GetPersonWithId
{
    public record GetPersonWithIdQuery(int Id) : IRequest<PersonDTO>;
    
}
