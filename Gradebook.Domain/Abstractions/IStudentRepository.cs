using Gradebook.Domain.Entities;

namespace Gradebook.Domain.Abstractions;

public interface IStudentRepository
{
    Task<Student> GetByIdAsync(int id, CancellationToken cancellation = default);
    Task<Student> GetByEmailAsync(string email, CancellationToken cancellation = default);
    Task<bool> IsAlreadyExistAsync(string email, CancellationToken cancellation = default);
    void Add(Student student);
    void Update(Student student);
    void Delete(Student student);
}
