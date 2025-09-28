using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Shared;

namespace PersonManagement.Application.Persons.Queries.GetPersonsList
{
    public record GetAllPersonsListQuery(
    string? SearchTerm,
    string? SortBy,
    bool SortDescending,
    int Page,
    int PageSize
    ) : IRequest<PagedResult<PersonDTO>>;
}
