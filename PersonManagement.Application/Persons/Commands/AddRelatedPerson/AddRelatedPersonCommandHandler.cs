using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandHandler : IRequestHandler<AddRelatedPersonCommand, int>
    {
        IUnitOfWork _unitOfWork;
        IPersonReadRepository _personReadRepository;
        public AddRelatedPersonCommandHandler(IUnitOfWork unitOfWork, IPersonReadRepository personReadRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personReadRepository = personReadRepository ?? throw new ArgumentNullException(nameof(personReadRepository));
        }
        public async Task<int> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var relatedPerson = await _personReadRepository.GetSingleOrDefaultAsync(p => p.Id == request.RelatedPersonId);

            if (relatedPerson is null)
            {
                throw new NotFoundException(nameof(relatedPerson), "Related Person not found.");
            }

            var person = Person.Create(
              request.FirstName,
              request.LastName,
              request.Gender,
              request.PersonalIdNumber,
              request.BirthDay,
              request.PhoneNumbers?
                        .Select(p => PhoneNumber.Create(p.Number, p.PhoneType))
                        .ToList(),
              null);

            _unitOfWork.PersonWriteRepository.Add(person);

            var relatedTo = RelatedPerson.Create(
                                person,
                                relatedPerson,
                                request.RelationshipType
            );

            _unitOfWork.RelatedPersonWriteRepository.Add(relatedTo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return person.Id;
        }
    }
}
