using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.RepoInterfaces;

namespace PersonManagement.Application.Experiences.Commands.AddExperienceToPerson
{
    public class AddExperienceToPersonCommandHandler : IRequestHandler<AddExperienceToPersonCommand, bool>
    {
        IUnitOfWork _unitOfWork;
        IPersonReadRepository _personReadRepository;
        public AddExperienceToPersonCommandHandler(IUnitOfWork unitOfWork, IPersonReadRepository personReadRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personReadRepository = personReadRepository;
        }
        public async Task<bool> Handle(AddExperienceToPersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetSingleOrDefaultAsync(p=>p.Id == request.PersonId, include: new[] { "Experiences" },
                        cancellationToken);

            if (person is null)
            { 
                throw new NotFoundException(nameof(person), "Person not found.");
            }
            person.AddExperience( request.Position,request.skills, request.StartDate, request.EndDate, request.CompanyName);
            _unitOfWork.PersonWriteRepository.Update(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
