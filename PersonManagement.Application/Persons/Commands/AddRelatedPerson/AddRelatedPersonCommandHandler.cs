using MediatR;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using System.Reflection;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandHandler : IRequestHandler<AddRelatedPersonCommand, int>
    {
        IUnitOfWork _unitOfWork;
        IPersonReadRepository _personReadRepository;
        public AddRelatedPersonCommandHandler(IUnitOfWork unitOfWork, IPersonReadRepository personReadRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));    
            _personReadRepository = personReadRepository;
        }
        public async  Task<int> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {

            var relatedPersonExists = await _personReadRepository.AnyAsync(p => p.Id == request.RelatedPersonId);

            if (!relatedPersonExists)
            {
                throw new Exception("Related person does not exist.");
            }

            // 1. Create Person without related persons
            var person = Person.Create(
            request.FirstName,
            request.LastName,
            request.Gender ?? null,
            request.PersonalIdNumber,
            request.BirthDay,
            request.PhoneNumbers?.Select(p => new PhoneNumber(p.Number, p.PhoneType)).ToList(),
            null
            );

            _unitOfWork.PersonWriteRepository.Add(person);
            await _unitOfWork.CompleteAsync(cancellationToken);

            var relatedPerson = new RelatedPerson(
                person.Id,
                request.RelatedPersonId,
                request.RelationshipType
            );

             _unitOfWork.RelatedPersonWriteRepository.Add(relatedPerson);
            await _unitOfWork.CompleteAsync(cancellationToken);

            //TODO: დდდ
            //TODO: rollback to add

            return person.Id;
        }
    }
    
    
}
