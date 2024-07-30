using System.Net;

namespace Gradebook.Domain.Exceptions;

public class StudentNotFoundException : GradebookException
{
    public int Id { get; set; }
    public StudentNotFoundException(int id) : base($"Student with ID {id} was not found.")
        => Id = id;

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
