using FluentValidation;

namespace Gradebook.Application.Commands.Grades.AddGrade;

public class AddGradeCommandValidation : AbstractValidator<AddGradeCommand>
{
    public AddGradeCommandValidation()
    {
        RuleFor(x => x.Name)
         .NotEmpty().WithMessage("Name is required.")
         .MaximumLength(120).WithMessage("Name cannot be longer than 120 characters.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.");
    }
}
