using FluentValidation.TestHelper;
using Gradebook.Application.Commands.Students.AddStudent;
using Gradebook.Domain.Abstractions;
using Moq;

namespace Gradebook.UnitTests.Commands.Students.AddStudent;

public class AddStudentCommandValidationTests
{
    private readonly Mock<IStudentRepository> _studentRepositoryMock;

    public AddStudentCommandValidationTests()
    {
        _studentRepositoryMock = new();
    }

    [Fact]
    public void ValidationResult_Should_Not_HaveAnyValidationErrors_WhenAddStudentCommandIsValidated()
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
            x => x.IsAlreadyExistAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new AddStudentCommandValidation();

        // Act
        var validationResult = validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ValidationResult_Should_HaveValidationErrorForEmail_WhenEmailIsEmpty()
    {
        // Arrange
        var command = new AddStudentCommand()
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = string.Empty,
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
            YearEnrolled = 2022
        };

        _studentRepositoryMock.Setup(
            x => x.IsAlreadyExistAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new AddStudentCommandValidation();

        // Act
        var validationResult = validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void ValidationResult_Should_HaveValidationErrorForEmail_WhenEmailHasMoreThan100Characters()
    {
        // Arrange
        var command = new AddStudentCommand()
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce mattis ultrices odio eu venenatis. Mauris ac sollicitudin nisi. Nam scelerisque ultricies dui. Donec sit amet tellus congue, hendrerit ex quis, consequat orci. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec sed ornare nulla, ac euismod turpis. Morbi consectetur elit turpis, vel auctor nibh rutrum in. Aenean venenatis, urna vel pellentesque accumsan, turpis massa maximus urna, ac facilisis justo massa et urna. Mauris vitae velit mattis, gravida risus id, accumsan justo. Sed quis consequat tortor, ut luctus dui. Class aptent taciti sociosqu ad litora torquent per conubia.",
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
            YearEnrolled = 2022
        };

        _studentRepositoryMock.Setup(
            x => x.IsAlreadyExistAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new AddStudentCommandValidation();

        // Act
        var validationResult = validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void ValidationResult_Should_HaveValidationErrorForEmail_WhenEmailIsNotValid()
    {
        // Arrange
        var command = new AddStudentCommand()
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "j.kowalski.gmail.com",
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
            YearEnrolled = 2022
        };

        _studentRepositoryMock.Setup(
            x => x.IsAlreadyExistAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new AddStudentCommandValidation();

        // Act
        var validationResult = validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Email);
    }
}
