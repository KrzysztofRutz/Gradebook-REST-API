using FluentValidation;
using Gradebook.Application.Commands.Students.AddStudent;

namespace Gradebook.Application.Commands.Students.UpdateStudent;

public class UpdateStudentCommandValidation : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

        RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Last name is required.")
           .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");

        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email name is required.")
           .MaximumLength(100).WithMessage("Last name cannot be longer than 50 characters.")
           .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.DateOfBirth)
           .NotEmpty().WithMessage("Date of birth is required.")
           .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-18))).WithMessage("Student must be at least 18 years old.");

        RuleFor(x => x.YearEnrolled)
           .NotEmpty().WithMessage("Year enrolled name is required.");
    }
}
