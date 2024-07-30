using System.Net;

namespace Gradebook.Domain.Exceptions;

public class GradeAlreadyExistsException : GradebookException
{
    public string Name { get; }
    public decimal Value { get; }
    public GradeAlreadyExistsException(string name, decimal value) 
        : base($"Grade with name {name} and value {value} already exists.")
    {
        Name = name;
        Value = value;
    }

    public override HttpStatusCode StatusCode =>HttpStatusCode.BadRequest;
}
