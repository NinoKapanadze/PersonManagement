using Moq;
using PersonManagement.Application.Persons.Queries.GetPersonsList;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Shared;
using System.Linq.Expressions;

namespace PersonManagement.Application.Tests
{
    public class GetAllPersonListQueryHandlerTests
    {
        private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
        private readonly GetAllPersonListQueryHandler _handler;

        public GetAllPersonListQueryHandlerTests()
        {
            _personReadRepositoryMock = new Mock<IPersonReadRepository>();
            _handler = new GetAllPersonListQueryHandler(_personReadRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnPagedPersons_When_PersonsExist()
        {
            // Arrange
            var person = PersonTestHelper.CreatePerson(1);

            var pagedResult = new PagedResult<Person>(
                new List<Person> { person },
                totalCount: 1,
                page: 1,
                pageSize: 10
            );

            _personReadRepositoryMock
                .Setup(r => r.GetPagedListAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<string[]>(),
                    It.IsAny<string?>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedResult);

            var query = new GetAllPersonsListQuery(
                Page: 1,
                PageSize: 10,
                SearchTerm: null,
                SortBy: null,
                SortDescending: false
            );

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Single(result.Items);
            Assert.Equal(1, result.TotalCount);
            Assert.Equal(person.FirstName, result.Items[0].FirstName);
            Assert.Equal(person.LastName, result.Items[0].LastName);
            _personReadRepositoryMock.Verify(r => r.GetPagedListAsync(
                1, 10, It.IsAny<Expression<Func<Person, bool>>>(),
                It.IsAny<string[]>(),
                null, false,
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_FilterPersons_When_SearchTermProvided()
        {
            // Arrange
            var person = PersonTestHelper.CreatePerson(1);
            person.Update("Johnny", "Doe", true, "12345678901", new DateOnly(1990, 1, 1), new List<PhoneNumber>());

            var pagedResult = new PagedResult<Person>(
                new List<Person> { person },
                totalCount: 1,
                page: 1,
                pageSize: 10
            );

            _personReadRepositoryMock
                .Setup(r => r.GetPagedListAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<string[]>(),
                    It.IsAny<string?>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedResult);

            var query = new GetAllPersonsListQuery(
                Page: 1,
                PageSize: 10,
                SearchTerm: "Johnny",
                SortBy: null,
                SortDescending: false
            );

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Single(result.Items);
            Assert.Equal("Johnny", result.Items[0].FirstName);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyResult_When_NoPersonsExist()
        {
            // Arrange
            var pagedResult = new PagedResult<Person>(
                new List<Person>(),
                totalCount: 0,
                page: 1,
                pageSize: 10
            );

            _personReadRepositoryMock
                .Setup(r => r.GetPagedListAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    It.IsAny<string[]>(),
                    It.IsAny<string?>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedResult);

            var query = new GetAllPersonsListQuery(
                Page: 1,
                PageSize: 10,
                SearchTerm: null,
                SortBy: null,
                SortDescending: false
            );

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
        }
    }
}
