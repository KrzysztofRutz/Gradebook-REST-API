﻿using FluentValidation;

namespace Gradebook.Application.Commands.Students.UpdateStudent;

public class UpdateStudentCommandValidation : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

        RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Last name is required.")
           .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");

        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email name is required.")
           .MaximumLength(100).WithMessage("Last name cannot be longer than 50 characters.")
           .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.DateOfBirth)
           .NotEmpty().WithMessage("Date of birth is required.")
           .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-18))).WithMessage("Student must be at least 18 years old.");

        RuleFor(x => x.YearEnrolled)
           .NotEmpty().WithMessage("Year enrolled name is required.");

        RuleFor(x => x.TypeOfStudies)
            .NotEmpty().WithMessage("Type of studies is required.")
            .Must(x => x == "Stationary" || x == "Part-time").WithMessage("Type of studies has not valid name.")
            .MaximumLength(10).WithMessage("Type of studies cannot be longer than 10 characters.");

        RuleFor(x => x.StreetName)
         .NotEmpty().WithMessage("Street name is required.")
         .MaximumLength(100).WithMessage("Street name cannot be longer than 100 characters.");

        RuleFor(x => x.StreetNumber)
            .NotEmpty().WithMessage("Street number is required.")
            .MaximumLength(10).WithMessage("Street number cannot be longer than 10 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City name cannot be longer than 50 characters.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.")
            .MaximumLength(10).WithMessage("Postal code cannot be longer than 10 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(50).WithMessage("Country name cannot be longer than 50 characters.");
    }
}
