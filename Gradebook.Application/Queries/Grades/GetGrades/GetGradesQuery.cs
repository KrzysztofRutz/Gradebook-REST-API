using Gradebook.Application.Configuration.Queries;
using Gradebook.Application.Dtos;
using MediatR;

namespace Gradebook.Application.Queries.Grades.GetGrades;

public record GetGradesQuery() : IRequest<IEnumerable<GradeDto>>
{
}
