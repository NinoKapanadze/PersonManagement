using Moq;
using PersonManagement.Application.DTOs;
using PersonManagement.Application.Persons.Queries.GetPersonsList;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetAllPersonListQueryHandlerTests
{
    private readonly Mock<IPersonReadRepository> _mockRepo = new ();
    private readonly GetAllPersonListQueryHandler _handler = new ();

    public GetAllPersonListQueryHandlerTests()
    {
        _handler = new GetAllPersonListQueryHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPagedResult_WhenDataExists()
    {
        // Arrange
        var request = new GetAllPersonsListQuery(
           SearchTerm: "Nino",
           SortBy: "LastName",
           SortDescending: false,
           Page: 1,
           PageSize: 10
       );

        var persons = new List<Person>
        {
             Person.Create("Nino", "Kapanadze", false, "01024078752", DateOnly.Parse("2000-07-07"),
                new List<PhoneNumber>
                {
                    PhoneNumber.Create("522334", PhoneType.House),
                    PhoneNumber.Create("+555dg678", PhoneType.Mobile)
                }
             )

        };

        var pagedPersons = new PagedResult<Person>(
            items: persons,
            totalCount: 1,
            page: 1,
            pageSize: 10
        );

        _mockRepo
            .Setup(r => r.GetPagedListAsync(
                request.Page, //pageindex
                request.PageSize, //pagesize
                request.SearchTerm, // filter
                null, //include
                null, //orderby
                request.SortDescending, //descending
                It.IsAny<CancellationToken>()
            ))
       .ReturnsAsync(persons
    ));
        
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Items);
        Assert.Equal("Nino", result.Items.First().FirstName);
    }
}

