using Moq;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Commands.CreatePerson;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;

namespace PersonManagement.Application.Tests
{
    public class CreatePersonCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
        private readonly CreatePersonCommandHandler _handler;

        public CreatePersonCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _personReadRepositoryMock = new Mock<IPersonReadRepository>();

            _handler = new CreatePersonCommandHandler(_unitOfWorkMock.Object, _personReadRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_CreatePerson_When_NotExists()
        {
            // Arrange
            var command = new CreatePersonCommand
            (
                "John",
                 "Doe",
                 true,
                 "12345678901",
                 new DateOnly(1990, 1, 1),
                 new List<DTOs.PhoneNumberDto>()
            );

            _personReadRepositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _unitOfWorkMock
                .Setup(u => u.PersonWriteRepository.Add(It.IsAny<Person>()))
                .Verifiable();

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            //.True(result > 0); 
            _unitOfWorkMock.Verify(u => u.PersonWriteRepository.Add(It.IsAny<Person>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_When_PersonAlreadyExists()
        {
            // Arrange
            var command = new CreatePersonCommand
            (
                 "Jane",
                 "Smith",
                 false,
                 "98765432101",
                new DateOnly(1995, 5, 10),
                new List<DTOs.PhoneNumberDto>()
            );

            _personReadRepositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Person, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<ObjectAlreadyExistsException>(() => _handler.Handle(command, CancellationToken.None));

            _unitOfWorkMock.Verify(u => u.PersonWriteRepository.Add(It.IsAny<Person>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}



