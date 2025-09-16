using Moq;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Commands.DeletePerson;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using System.Linq.Expressions;

namespace PersonManagement.Application.Tests
{
    public class DeletePersonCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
        private readonly DeletePersonCommandHandler _handler;

        public DeletePersonCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _personReadRepositoryMock = new Mock<IPersonReadRepository>();

            _handler = new DeletePersonCommandHandler(
                _unitOfWorkMock.Object,
                _personReadRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_DeletePerson_When_PersonExists()
        {
            // Arrange
            var person = PersonTestHelper.CreatePerson(1);

            var command = new DeletePersonCommand(1);

            _personReadRepositoryMock
                .Setup(r => r.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(person);

            _unitOfWorkMock
                .Setup(u => u.PersonWriteRepository.Delete(person))
                .Returns(true);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.PersonWriteRepository.Delete(person), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFoundException_When_PersonDoesNotExist()
        {
            // Arrange
            var command = new DeletePersonCommand(99);

            _personReadRepositoryMock
                .Setup(r => r.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Person, bool>>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));

            _unitOfWorkMock.Verify(u => u.PersonWriteRepository.Delete(It.IsAny<Person>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
