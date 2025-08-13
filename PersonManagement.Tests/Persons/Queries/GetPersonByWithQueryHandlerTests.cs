using Moq;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Queries.GetPersonWithId;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Shared;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

public class GetPersonWithIdQueryHandlerTests
{
    private readonly Mock<IPersonReadRepository> _mockRepo;
    private readonly GetPersonWithIdQueryHandler _handler;

    public GetPersonWithIdQueryHandlerTests()
    {
        _mockRepo = new Mock<IPersonReadRepository>();
        _handler = new GetPersonWithIdQueryHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPersonDTO_WhenPersonExists()
    {
        // Arrange
        var request = new GetPersonWithIdQuery(1);

        var person = Person.Create(
            "Nino",
            "Kapanadze",
            false,
            "01024078752",
            DateOnly.Parse("2000-07-07"),
            new List<PhoneNumber>
            {
                PhoneNumber.Create("522334", PhoneType.House),
                PhoneNumber.Create("+555678", PhoneType.Mobile)
            }
        );

        person.Id = 1;

        var related = Person.Create(
            "Giorgi",
            "Smith",
            true,
            "123456789",
            DateOnly.Parse("1995-05-05"),
            new List<PhoneNumber>
            {
                PhoneNumber.Create("555000", PhoneType.Mobile)
            }
        );
        //related.Id = 2;

        //person.RelatedPersons = new List<RelatedPerson>
        //{
        //    RelatedPerson.Create(person, related, "Friend")
        //};

        _mockRepo
            .Setup(r => r.GetPersonWithDetailsAsync(request.Id))
            .ReturnsAsync(person);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Nino", result.FirstName);
        Assert.Equal("Kapanadze", result.LastName);
        Assert.Equal(2, result.PhoneNumbers.Count);
        Assert.Single(result.RelatedPersons);
        //Assert.Equal("Friend", result.RelatedPersons[0].RelationshipType);
        Assert.Equal("Giorgi", result.RelatedPersons[0].RelatedPerson.FirstName);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var request = new GetPersonWithIdQuery(99);
        _mockRepo
            .Setup(r => r.GetPersonWithDetailsAsync(request.Id))
            .ReturnsAsync((Person)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(
            () => _handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Person with Id 99 not found.", ex.Message);
    }
}

