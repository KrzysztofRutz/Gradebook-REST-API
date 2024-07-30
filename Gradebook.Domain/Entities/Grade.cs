namespace Gradebook.Domain.Entities;

public class Grade : Entity
{
    public string Name { get; set; }
    public decimal Value { get; set; }
    public ICollection<Student> Students { get; set; }
}
