using Gradebook.Application.Configuration.Queries;
using Gradebook.Application.Dtos;

namespace Gradebook.Application.Queries.Grades.GetGradeStudents;

public record class GetGradeStudentsQuery (int Id) : IQuery<IEnumerable<StudentDto>>;
