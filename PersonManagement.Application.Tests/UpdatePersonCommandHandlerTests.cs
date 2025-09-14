using Moq;
using PersonManagement.Application.DTOs;
using PersonManagement.Application.Exceptions;
using PersonManagement.Application.Persons.Commands.UpdatePerson;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Shared;

namespace PersonManagement.Application.Tests
{
    public class UpdatePersonCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
        private readonly UpdatePersonCommandHandler _handler;

        public UpdatePersonCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _personReadRepositoryMock = new Mock<IPersonReadRepository>();

            _handler = new UpdatePersonCommandHandler(
                _unitOfWorkMock.Object,
                _personReadRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_UpdatePerson_AndReturnDto_WhenPersonExists()
        {
            // Arrange

            var person = PersonTestHelper.CreatePerson();

            var command = new UpdatePersonCommand(
                Id: 1,
                FirstName: "Johnny",
                LastName: "Doe",
                Gender: true,
                PersonalIdNumber: "12345678901",
                BirthDay: new DateOnly(1990, 1, 1),
                PhoneNumbers: new List<PhoneNumberDto>
                {
                    new PhoneNumberDto("9999999", PhoneType.House)
                }
            );

            _personReadRepositoryMock
                .Setup(r => r.GetSingleOrDefaultAsync(
                    It.Is<Func<Person, bool>>(p => p.Id == 1),
                    It.IsAny<string[]>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(person);

            _unitOfWorkMock
                .Setup(u => u.PersonWriteRepository.Update(person));

            _unitOfWorkMock
                 .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal("Johnny", result.FirstName);
            Assert.Single(result.PhoneNumbers);
            Assert.Equal("9999999", result.PhoneNumbers[0].Number);

            _unitOfWorkMock.Verify(u => u.PersonWriteRepository.Update(person), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFoundException_WhenPersonDoesNotExist()
        {
            // Arrange
            var command = new UpdatePersonCommand(
                Id: 99,
                FirstName: "Johnny",
                LastName: "Doe",
                Gender: true,
                PersonalIdNumber: "12345678901",
                BirthDay: new DateOnly(1990, 1, 1),
                PhoneNumbers: new List<PhoneNumberDto>()
            );

            _personReadRepositoryMock
                .Setup(r => r.GetSingleOrDefaultAsync(
                    It.IsAny<Func<Person, bool>>(),
                    It.IsAny<string[]>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Person?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}


