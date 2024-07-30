using Gradebook.Application.Configuration.Commands;

namespace Gradebook.Application.Commands.Grades.GiveStudentGrade;

public record class GiveStudentGradeCommand(int Id, int GradeId) : ICommand;
