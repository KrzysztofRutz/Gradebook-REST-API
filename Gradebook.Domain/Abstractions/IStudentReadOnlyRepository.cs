using Gradebook.Domain.Entities;

namespace Gradebook.Domain.Abstractions;

public interface IStudentReadOnlyRepository
{
    Task<IEnumerable<Student>> GetAllAsync(int YearEnrolled = 0, CancellationToken cancellation = default);

    Task<IEnumerable<Student>> GetAllWithDetailsAsync(CancellationToken cancellation = default);
}
