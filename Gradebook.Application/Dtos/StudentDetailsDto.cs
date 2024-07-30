namespace Gradebook.Application.Dtos;

public class StudentDetailsDto : StudentDto
{
    public AddressDto Address { get; set; }
    public GradeDto Grade { get; set; }
}
