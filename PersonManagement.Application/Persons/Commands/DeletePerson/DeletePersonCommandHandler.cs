using MediatR;
using PersonManagement.Application.RepoInterfaces;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonReadRepository _personReadRepository;
        public DeletePersonCommandHandler(IUnitOfWork unitOfWork, IPersonReadRepository personReadRepository)
        {
             _unitOfWork = unitOfWork;
            _personReadRepository = personReadRepository;
        }
        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await  _personReadRepository.GetSingleAsync(p => p.Id == request.Id);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
            }
            var result = _unitOfWork.PersonWriteRepository.Delete(person);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return result;
        }
    }
}
