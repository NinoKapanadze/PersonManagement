using MediatR;
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
            _unitOfWork = unitOfWork;
            _personReadRepository = personReadRepository;
        }
        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            bool exists = await _personReadRepository.AnyAsync(
                    p => p.PersonalIdNumber == request.PersonalIdNumber
            ); 
            
            var person = Person.Create(
            request.FirstName,
            request.LastName,
            request.Gender ?? null,
            request.PersonalIdNumber,
            request.BirthDay,
            request.PhoneNumbers?.Select(p => new PhoneNumber(p.Number, p.PhoneType)).ToList(),
            null
            );

           var p =  _unitOfWork.PersonWriteRepository.Add(person);

            await _unitOfWork.CompleteAsync();

            return p.Id;
        }
    }
}
