using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonReadRepository _personReadRepository;
        public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonReadRepository personReadRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personReadRepository = personReadRepository ?? throw new ArgumentNullException(nameof(personReadRepository));
        }
        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            if (await _personReadRepository.AnyAsync(p => p.PersonalIdNumber == request.PersonalIdNumber))
            {
                throw new ObjectAlreadyExistsException($"Person with PersonalIdNumber {request.PersonalIdNumber} already exists.");
            }

            var person = Person.Create(
            request.FirstName,
            request.LastName,
            request.Gender,
            request.PersonalIdNumber,
            request.BirthDay,
            request.PhoneNumbers?.Select(p => PhoneNumber.Create(p.Number, p.PhoneType)).ToList(),
            null
            );

            _unitOfWork.PersonWriteRepository.Add(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return person.Id;
        }
    }
}
