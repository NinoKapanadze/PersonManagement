using MediatR;
using PersonManagement.Application.DTOs;
using PersonManagement.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
