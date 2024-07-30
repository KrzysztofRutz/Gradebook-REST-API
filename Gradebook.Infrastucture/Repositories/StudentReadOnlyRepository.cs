using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Infrastructure.Repositoires;

internal class StudentReadOnlyRepository : IStudentReadOnlyRepository
{
    private readonly GradebookDbContext _dbContext;

    public StudentReadOnlyRepository(GradebookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellation = default)
    {
        return await _dbContext.Students.AsNoTracking().ToListAsync(cancellation);
    }

    public async Task<IEnumerable<Student>> GetAllWithDetailsAsync(CancellationToken cancellation = default)
    {
        return await _dbContext.Students
            .Include(x => x.Address)
            .Include(x => x.Grade)
            .AsNoTracking().ToListAsync(cancellation);
    }
}
