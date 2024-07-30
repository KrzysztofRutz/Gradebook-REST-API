using Gradebook.Domain.Entities;

namespace Gradebook.Domain.Abstractions;

public interface IStudentReadOnlyRepository
{
    Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellation = default);

    Task<IEnumerable<Student>> GetAllWithDetailsAsync(CancellationToken cancellation = default);

}
