using MediatR;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public record DeletePersonCommand(int Id) : IRequest<bool>;
    
    
}
