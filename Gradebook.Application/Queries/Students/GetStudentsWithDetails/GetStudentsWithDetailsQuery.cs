using Gradebook.Application.Configuration.Queries;
using Gradebook.Application.Dtos;

namespace Gradebook.Application.Queries.Students.GetStudentsWithDetails;

public record GetStudentsWithDetailsQuery : IQuery<IEnumerable<StudentDetailsDto>>;
