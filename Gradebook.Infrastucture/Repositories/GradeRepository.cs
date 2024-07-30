using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Infrastructure.Repositories;

internal class GradeRepository : IGradeRepository
{
    private readonly GradebookDbContext _dbContext;

    public GradeRepository(GradebookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Grade> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _dbContext.Grades.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<Grade> GetByIdAsyncWithStudentsAsync(int id, CancellationToken cancellationToken)
       => await _dbContext.Grades.Include(x => x.Students).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<bool> IsAlreadyExistAsync(string name, decimal value, CancellationToken cancellationToken)
        => await _dbContext.Grades.SingleOrDefaultAsync(x => x.Name == name && x.Value == value, cancellationToken) is not null;

    public void Add(Grade grade)
        => _dbContext.Grades.Add(grade);

    public void Delete(Grade grade)
        => _dbContext.Grades.Remove(grade);
}
