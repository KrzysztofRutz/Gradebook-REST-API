﻿using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Infrastructure.Repositoires;

internal class StudentRepository : IStudentRepository
{
    private readonly GradebookDbContext _dbContext;

    public StudentRepository(GradebookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        return await _dbContext.Students
            .Include(x => x.Grade)
            .Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Id == id, cancellation);
    }

    public async Task<Student> GetByEmailAsync(string email, CancellationToken cancellation = default)
    {
        return await _dbContext.Students.Include(x => x.Grade).SingleOrDefaultAsync(x => x.Email == email, cancellation);
    }

    public Task<bool> IsAlreadyExistAsync(string email, CancellationToken cancellation = default)
    {
        return _dbContext.Students.AnyAsync(x => x.Email == email, cancellation);
    }

    public void Add(Student student)
    {
        _dbContext.Students.Add(student);
    }

    public void Update(Student student)
    {
        _dbContext.Students.Update(student);
    }

    public void Delete(Student student)
    {
        _dbContext.Students.Remove(student);
    }
}
