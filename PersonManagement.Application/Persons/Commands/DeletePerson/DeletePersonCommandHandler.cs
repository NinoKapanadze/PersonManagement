using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.RepoInterfaces;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonReadRepository _personReadRepository;
        public DeletePersonCommandHandler(IUnitOfWork unitOfWork, IPersonReadRepository personReadRepository)
        {
             _unitOfWork = unitOfWork?? throw new ArgumentNullException(nameof(unitOfWork));
            _personReadRepository = personReadRepository?? throw new ArgumentNullException(nameof(personReadRepository));
        }
        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await  _personReadRepository.GetSingleOrDefaultAsync(p => p.Id == request.Id);

            if (person == null)
            {
                throw new NotFoundException($"Person with ID {request.Id} not found.");
            }
            var result = _unitOfWork.PersonWriteRepository.Delete(person);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
