using System.Net;

namespace Gradebook.Domain.Exceptions;

public abstract class GradebookException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    public GradebookException(string messege) : base(messege)
    {
        
    }
}
