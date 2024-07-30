using Gradebook.Domain.Entities;

namespace Gradebook.Domain.Abstractions;

public interface IGradeReadOnlyRepository
{
    Task<IEnumerable<Grade>> GetAllAsync(CancellationToken cancellationToken = default);
}
