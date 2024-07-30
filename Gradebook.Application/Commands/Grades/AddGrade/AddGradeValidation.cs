using FluentValidation;

namespace Gradebook.Application.Commands.Grades.AddGrade;

public class AddGradeValidation : AbstractValidator<AddGradeCommand>
{
    public AddGradeValidation()
    {
        RuleFor(x => x.Name)
         .NotEmpty().WithMessage("Name is required.")
         .MaximumLength(15).WithMessage("Name cannot be longer than 15 characters.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.");
    }
}
