using Gradebook.Application.Configuration.Commands;
using Gradebook.Application.Dtos;

namespace Gradebook.Application.Commands.Grades.AddGrade;

public class AddGradeCommand : ICommand<GradeDto>
{
    public string Name { get; set; }
    public decimal Value { get; set; }
}
