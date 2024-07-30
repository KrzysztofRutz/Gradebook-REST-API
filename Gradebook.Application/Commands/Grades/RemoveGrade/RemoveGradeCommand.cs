using Gradebook.Application.Configuration.Commands;

namespace Gradebook.Application.Commands.Grades.RemoveGrade;

public record RemoveGradeCommand(int Id) : ICommand
{
}
