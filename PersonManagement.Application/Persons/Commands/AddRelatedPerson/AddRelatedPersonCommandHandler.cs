using MediatR;
using PersonManagement.Application.Exceptions;
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
            _personReadRepository = personReadRepository ?? throw new ArgumentNullException(nameof(personReadRepository));
        }
        public async  Task<int> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var relatedPersonExists = await _personReadRepository.AnyAsync(p => p.Id == request.RelatedPersonId);

            if (!relatedPersonExists)
            {
                throw new NotFoundException("Related person does not exist.");
            }

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
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
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var relatedTo =  RelatedPerson.Create(
                                    person.Id,
                                    request.RelatedPersonId,
                                    request.RelationshipType
                );

                _unitOfWork.RelatedPersonWriteRepository.Add(relatedTo);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return person.Id;
      
            }
            catch 
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
              
        }
    }
}
