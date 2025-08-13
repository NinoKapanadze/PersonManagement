using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, PersonDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonReadRepository _personReadRepository;  
        public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonReadRepository personReadRepository)
        {
            _unitOfWork = unitOfWork;
            _personReadRepository = personReadRepository;
        }
        public async Task<PersonDTO> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetSingleAsync(p=> p.Id == request.Id);

            if (person == null)
                throw new KeyNotFoundException($"Person with Id {request.Id} not found.");
            person.Update(
                 request.FirstName,
                 request.LastName,
                 request.Gender,
                 request.PersonalIdNumber,
                 request.BirthDay,
                 request.PhoneNumbers.Select(pn => new PhoneNumber(pn.Number, pn.PhoneType)).ToList()
            );


            _unitOfWork.PersonWriteRepository.Update(person);
            await _unitOfWork.CompleteAsync();

            return new PersonDTO(
                Id: person.Id,
                FirstName: person.FirstName,
                LastName: person.LastName,
                Gender: person.Gender,
                PersonalIdNumber: person.PersonalIdNumber,
                BirthDay: person.BirthDay,
                PhoneNumbers: person.PhoneNumbers.Select(pn => new PhoneNumberDto(
                    Number: pn.Number,
                    PhoneType: pn.PhoneType
                )).ToList(),
                new List<RelatedPersonDTO>()
            );
        }
    }
}
