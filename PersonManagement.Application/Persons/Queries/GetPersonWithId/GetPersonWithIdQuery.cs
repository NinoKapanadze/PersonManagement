using MediatR;
using PersonManagement.Application.DTOs;

namespace PersonManagement.Application.Persons.Queries.GetPersonWithId
{
    public class GetPersonWithIdQuery : IRequest<PersonDTO>
    {
        public int Id { get; set; }
    }
}
