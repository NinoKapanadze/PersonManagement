using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Application.Exceptions;
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
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personReadRepository = personReadRepository ?? throw new ArgumentNullException(nameof(personReadRepository));
        }
        public async Task<PersonDTO> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetSingleOrDefaultAsync(
                        p => p.Id == request.Id,
                        include: new []{ "PhoneNumbers" });

            if (person == null)
                throw new NotFoundException($"Person with Id {request.Id} not found.");

            person.Update(
                 request.FirstName,
                 request.LastName,
                 request.Gender,
                 request.PersonalIdNumber,
                 request.BirthDay,
                 request.PhoneNumbers.Select(pn => PhoneNumber.Create(pn.Number, pn.PhoneType)).ToList()
            );

            if(request.PhoneNumbers is not null) //თუ ცარიელია ტელეფონებს ვტოვებთ უცვლელად
            {
                person.UpdatePhoneNumbers(
                    request.PhoneNumbers.Select(pn => PhoneNumber.Create(pn.Number, pn.PhoneType)).ToList()
                );
            }

            _unitOfWork.PersonWriteRepository.Update(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PersonDTO(
                Id: person.Id,
                FirstName: person.FirstName,
                LastName: person.LastName,
                Gender: person.Gender,
                PersonalIdNumber: person.PersonalIdNumber,
                BirthDay: person.BirthDay,
                PhoneNumbers: person.PhoneNumbers.Where(pn => !pn.IsDeleted)
                                                 .Select(pn => new PhoneNumberDto(
                    Number: pn.Number,
                    PhoneType: pn.PhoneType
                )).ToList(),
                new List<RelatedPersonDTO>()
            );
        }
    }
}
