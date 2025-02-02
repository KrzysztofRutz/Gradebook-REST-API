﻿namespace Gradebook.Domain.Entities;

public class Student : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int YearEnrolled { get; set; }

    public int? GradeId { get; set; }
    public Grade Grade { get; set; }

    public Address Address { get; set; }

    public string TypeOfStudies { get; set; }
}
