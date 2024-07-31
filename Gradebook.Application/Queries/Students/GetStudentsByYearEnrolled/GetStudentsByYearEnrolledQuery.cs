using Gradebook.Application.Dtos;
using MediatR;

namespace Gradebook.Application.Queries.Students.GetStudentsByYearEnrolled;

public record GetStudentsByYearEnrolledQuery(int YearEnrolled) : IRequest<IEnumerable<StudentDto>>
{
}
