using AutoMapper;
using Gradebook.Application.Commands.Students.AddStudent;
using Gradebook.Application.Configuration.Mappings;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Domain.Exceptions;
using Moq;

namespace Gradebook.UnitTests.Commands.Students.AddStudent;

public class AddStudentCommandHandlerTests
{
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;

    public AddStudentCommandHandlerTests()
    {
        _studentRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapper = MapperHelper.CreateMapper(new StudentMappingProfile());
    }

    [Fact]
    public async Task Handle_Should_CallAddOnReposiotry_WhenEmailIsUnique() 
    {
        // Arrange
        var command = new AddStudentCommand()
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "j.kowalski@gmail.com",
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
            YearEnrolled = 2022
        };

        _studentRepositoryMock.Setup(
            x => x.Add(It.IsAny<Student>()));

        _studentRepositoryMock.Setup(
            x => x.IsAlreadyExistAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new AddStudentCommandHandler(
            _studentRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapper);

        // Act 
        var studentDto = await handler.Handle(command, default);

        // Assert
        _studentRepositoryMock.Verify(
            x => x.Add(It.Is<Student>(x => x.Id == studentDto.Id)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrowStudentAlreadyExistsException_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new AddStudentCommand()
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "j.kowalski@gmail.com",
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
            YearEnrolled = 2022
        };

        _studentRepositoryMock.Setup(
            x => x.Add(It.IsAny<Student>()));

        _studentRepositoryMock.Setup(
            x => x.IsAlreadyExistAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new AddStudentCommandHandler(
            _studentRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapper);

        // Act & Assert
        await Assert.ThrowsAsync<StudentAlreadyExistsException>(async () => await handler.Handle(command, default));
    }
}
