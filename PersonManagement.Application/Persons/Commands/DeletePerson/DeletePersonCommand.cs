using MediatR;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
