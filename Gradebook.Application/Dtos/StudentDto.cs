﻿namespace Gradebook.Application.Dtos;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public int YearEnrolled { get; set; }
    public string TypeOfStudies { get; set; }
}
