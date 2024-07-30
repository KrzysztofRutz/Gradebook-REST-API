using AutoMapper;
using FluentAssertions;
using Gradebook.Application.Configuration.Mappings;
using Gradebook.Application.Queries.Students.GetStudentById;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Moq;

namespace Gradebook.UnitTests.Queries.Students.GetStudentById;

public class GetStudentByIdQueryHandlerTests
{
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly IMapper _mapper;

    public GetStudentByIdQueryHandlerTests()
    {
        _studentRepositoryMock = new();
        _mapper = MapperHelper.CreateMapper(new StudentMappingProfile());
    }

    [Fact]
    public async Task Handle_Should_CallGetByIdAsyncOnRepository_WhenGetStudentByIdQuery()
    {
        // Arrange
        _studentRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Student());

        var handler = new GetStudentByIdQueryHandler(
            _studentRepositoryMock.Object,
            _mapper);

        // Act
        await handler.Handle(new GetStudentByIdQuery(1), default);

        // Assert
        _studentRepositoryMock.Verify(
           x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),
           Times.Once
           );
    }

    [Fact]
    public async Task Handle_Should_ReturnStudent_WhenGetStudentByIdQuery()
    {
        // Arrange
        var student = new Student()
        {
            Id = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "j.kowalski@gmail.com",
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
            YearEnrolled = 2022,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _studentRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(student);

        var handler = new GetStudentByIdQueryHandler(
            _studentRepositoryMock.Object,
            _mapper);

        // Act
        var studentDto = await handler.Handle(new GetStudentByIdQuery(1), default);

        // Assert
        studentDto.Should().NotBeNull();
        studentDto.Id.Should().Be(student.Id);
        studentDto.FirstName.Should().Be(student.FirstName);
        studentDto.LastName.Should().Be(student.LastName);
        studentDto.Email.Should().Be(student.Email);
        int studentAge = DateTime.Now.Year - student.DateOfBirth.ToDateTime(TimeOnly.Parse("00:00")).Year;
        studentDto.Age.Should().Be(studentAge);
        studentDto.YearEnrolled.Should().Be(student.YearEnrolled);
    }

    [Fact]
    public async Task Handle_Should_ReturnNull_WhenGetStudentByIdQuery()
    {
        // Arrange
        _studentRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Student)null);

        var handler = new GetStudentByIdQueryHandler(
            _studentRepositoryMock.Object,
            _mapper);

        // Act
        var studentDto = await handler.Handle(new GetStudentByIdQuery(1), default);

        // Assert
        studentDto.Should().BeNull();   
    }
}
