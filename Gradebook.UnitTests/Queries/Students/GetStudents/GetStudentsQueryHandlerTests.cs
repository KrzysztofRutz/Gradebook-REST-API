using AutoMapper;
using FluentAssertions;
using Gradebook.Application.Configuration.Mappings;
using Gradebook.Application.Queries.Students.GetStudents;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Moq;

namespace Gradebook.UnitTests.Queries.Students.GetStudents;

public class GetStudentsQueryHandlerTests
{
    private readonly Mock<IStudentReadOnlyRepository> _studentReadOnlyRepositoryMock;
    private readonly IMapper _mapper;

    public GetStudentsQueryHandlerTests()
    {
        _studentReadOnlyRepositoryMock = new();
        _mapper = MapperHelper.CreateMapper(new StudentMappingProfile());
    }

    [Fact]
    public async Task Handle_Should_CallGetAllAsyncOnRepository_WhenGetStudentsQuery()
    {
        // Arrange
        _studentReadOnlyRepositoryMock.Setup(
            x => x.GetAllAsync(
                It.IsAny<CancellationToken>())).ReturnsAsync(Enumerable.Empty<Student>);

        var handler = new GetStudentsQueryHandler(
            _studentReadOnlyRepositoryMock.Object,
            _mapper);

        // Act
        await handler.Handle(new GetStudentsQuery(), default);

        // Assert
        _studentReadOnlyRepositoryMock.Verify(
           x => x.GetAllAsync(It.IsAny<CancellationToken>()),
           Times.Once
           );
    }

    [Fact]
    public async Task Handle_Should_ReturnNotEmptyCollection_WhenGetStudentsQuery()
    {
        // Arrange
        var students = new List<Student>()
        {
            new Student()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "j.kowalski@gmail.com",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1990,1,1)),
                YearEnrolled = 2022,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Student()
            {
                Id = 2,
                FirstName = "Adam",
                LastName = "Nowak",
                Email = "a.nowak@gmail.com",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1995,1,1)),
                YearEnrolled = 2020,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        };

        _studentReadOnlyRepositoryMock.Setup(
           x => x.GetAllAsync(
               It.IsAny<CancellationToken>())).ReturnsAsync(students);

        var handler = new GetStudentsQueryHandler(
            _studentReadOnlyRepositoryMock.Object,
            _mapper);

        // Act
        var studentsDto = await handler.Handle(new GetStudentsQuery(), default);

        // Assert
        studentsDto.Should().NotBeNullOrEmpty();
    }
}
