using Gradebook.Domain.Entities;

namespace Gradebook.Domain.Abstractions;

public interface IGradeRepository
{
    Task<IEnumerable<Grade>> Get(CancellationToken cancellationToken);

    Task<Grade> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Grade> GetByIdAsyncWithStudentsAsync(int id, CancellationToken cancellationToken);

    Task<bool> IsAlreadyExistAsync(string name, decimal value, CancellationToken cancellationToken);

    void Add(Grade grade);

    void Delete(Grade grade);
}
