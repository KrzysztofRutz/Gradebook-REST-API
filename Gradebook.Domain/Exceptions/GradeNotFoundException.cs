using System.Net;

namespace Gradebook.Domain.Exceptions;

public class GradeNotFoundException : GradebookException
{
    public int Id { get; }
    public GradeNotFoundException(int id) 
        : base($"Grade with ID {id} was not found.")
    {
        Id = id;
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
