using AutoMapper;
using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.RepoInterfaces;

namespace PersonManagement.Application.Persons.Queries.GetPersonWithId
{
    public class GetPersonWithIdQueryHandler : IRequestHandler<GetPersonWithIdQuery, PersonDTO>
    {
        private readonly IPersonReadRepository _personReadRepository;
        public GetPersonWithIdQueryHandler(IPersonReadRepository personReadRepository)
        {
            _personReadRepository = personReadRepository ?? throw new ArgumentNullException(nameof(personReadRepository));
        }
        public async Task<PersonDTO> Handle(GetPersonWithIdQuery request, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetPersonWithDetailsAsync(request.Id);

            if (person == null)
            {
                throw new NotFoundException($"Person with Id {request.Id} not found.");
            }

            var result = new PersonDTO(
                    Id : person.Id,
                    FirstName: person.FirstName,
                    LastName: person.LastName,
                    Gender: person.Gender,
                    PersonalIdNumber: person.PersonalIdNumber,
                    BirthDay: person.BirthDay,

                    PhoneNumbers: person.PhoneNumbers?.Select(p => new PhoneNumberDto(
                        Number: p.Number,
                        PhoneType: p.PhoneType
                    )).ToList() ?? new List<PhoneNumberDto>(),

                    RelatedPersons: person.RelatedPersons?.Where(rp => rp.RelatedTo != null)
                                                          .Select(rp => new RelatedPersonDTO(
                        RelatedPerson: new PersonDTO(
                            Id: rp.RelatedTo.Id,
                            FirstName: rp.RelatedTo.FirstName,
                            LastName: rp.RelatedTo.LastName,
                            Gender: rp.RelatedTo.Gender,
                            PersonalIdNumber: rp.RelatedTo.PersonalIdNumber,
                            BirthDay: rp.RelatedTo.BirthDay,
                            PhoneNumbers: rp.RelatedTo.PhoneNumbers?
                                    .Select(pn => new PhoneNumberDto(
                                        Number: pn.Number,
                                        PhoneType: pn.PhoneType
                                    ))
                                    .ToList() ?? new List<PhoneNumberDto>(),
                            RelatedPersons: new List<RelatedPersonDTO>() 
                        ),
                        RelationshipType: rp.RelationshipType
                    )).ToList() ?? new List<RelatedPersonDTO>()
                );

            return result;
            //TODO: logireba
        }
    }
}
