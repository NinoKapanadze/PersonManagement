using Moq;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Queries.GetPersonWithId;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Shared;

namespace PersonManagement.Application.Tests
{
    public class GetPersonWithIdQueryHandlerTests
    {
        private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
        private readonly GetPersonWithIdQueryHandler _handler;

        public GetPersonWithIdQueryHandlerTests()
        {
            _personReadRepositoryMock = new Mock<IPersonReadRepository>();
            _handler = new GetPersonWithIdQueryHandler(_personReadRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnPerson_When_PersonExists()
        {
            // Arrange
            var query = new GetPersonWithIdQuery(1);

            var person = Person.Create
            (
                "John",
                "Doe",
                true,
                "12345678901",
                new DateOnly(1990, 1, 1),
                new List<PhoneNumber>
                {
                     PhoneNumber.Create("5551234", PhoneType.Mobile)
                },
                new List<RelatedPerson>()
            );

            typeof(Person)
               .GetProperty("Id")!
               .SetValue(person, 1);


            var relatedPerson = Person.Create
            (
                "Jane",
                "Doe",
                false,
                "10987654321",
                new DateOnly(1992, 2, 2),
                new List<PhoneNumber>(),
                new List<RelatedPerson>()
            );

            typeof(Person).GetProperty("Id")!.SetValue(relatedPerson, 2);

            var relation = RelatedPerson.Create(person, relatedPerson, RelationshipType.Relative);

            person.RelatedPersons.Add(relation);

            _personReadRepositoryMock
                .Setup(r => r.GetPersonWithDetailsAsync(query.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(person);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Single(result.PhoneNumbers);
            Assert.Single(result.RelatedPersons);
            Assert.Equal("Jane", result.RelatedPersons[0].RelatedPerson.FirstName);
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFound_When_PersonDoesNotExist()
        {
            // Arrange
            var query = new GetPersonWithIdQuery(99);

            _personReadRepositoryMock
                .Setup(r => r.GetPersonWithDetailsAsync(query.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
