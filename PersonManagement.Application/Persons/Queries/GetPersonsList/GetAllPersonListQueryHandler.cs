using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Shared;

namespace PersonManagement.Application.Persons.Queries.GetPersonsList
{
    public class GetAllPersonListQueryHandler : IRequestHandler<GetAllPersonsListQuery, PagedResult<PersonDTO>>
    {
        private readonly IPersonReadRepository _personReadRepository;
        private static readonly string[] includeProperties = { "PhoneNumbers" };

        public GetAllPersonListQueryHandler(IPersonReadRepository personReadRepository)
        {
            _personReadRepository = personReadRepository ?? throw new ArgumentNullException(nameof(personReadRepository));
        }

        public async Task<PagedResult<PersonDTO>> Handle(GetAllPersonsListQuery request, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetPagedListAsync(
                pageIndex: request.Page,
                pageSize: request.PageSize,
                filter: p => string.IsNullOrEmpty(request.SearchTerm)
                    || p.FirstName.Contains(request.SearchTerm)
                    || p.LastName.Contains(request.SearchTerm)
                    || p.PersonalIdNumber.Contains(request.SearchTerm),
                includeProperties: includeProperties,
                orderBy: request.SortBy,
                descending: request.SortDescending,
                cancellationToken: cancellationToken
            );

            var personDtos = person.Items.Select(p => new PersonDTO(
                Id: p.Id,
                FirstName: p.FirstName,
                LastName: p.LastName,
                Gender: p.Gender,
                PersonalIdNumber: p.PersonalIdNumber,
                BirthDay: p.BirthDay,
                PhoneNumbers: p.PhoneNumbers.Select(ph => new PhoneNumberDto(
                    Number: ph.Number,
                    PhoneType: ph.PhoneType
                )).ToList(),
                RelatedPersons: new List<RelatedPersonDTO>(),
                Experiences: new List<ExperienceDTO>()
            )).ToList();

            return new PagedResult<PersonDTO>(
                items: personDtos,
                totalCount: person.TotalCount,
                page: person.Page,
                pageSize: person.PageSize
            );
        }
    }
}
