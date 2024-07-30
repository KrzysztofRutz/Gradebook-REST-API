using FluentAssertions;
using Gradebook.Application.Commands.Students.AddStudent;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Entities;
using Gradebook.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Gradebook.IntegrationTests.Controllers;

public class StudentControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public StudentControllerTests(WebApplicationFactory<Program> factory)
    {
        _webApplicationFactory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services
                    .SingleOrDefault(services => services.ServiceType == typeof(DbContextOptions<GradebookDbContext>));
                    services.Remove(dbContextOptions);
                    services.AddDbContext<GradebookDbContext>(options => options.UseInMemoryDatabase("GradebookDb"));
                });
            });
        _httpClient = _webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Should_ReturnListOfStudentsAndStatusCoseOK()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<GradebookDbContext>();

        _dbContext.Students.AddRange(
            new Student()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "j.kowalski@gmail.com",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
                YearEnrolled = 2022,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Student()
            {
                FirstName = "Adam",
                LastName = "Nowak",
                Email = "a.nowak@gmail.com",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1995, 1, 1)),
                YearEnrolled = 2020,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        await _dbContext.SaveChangesAsync();

        // Act
        var response = await _httpClient.GetAsync("/api/students");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<StudentDto>>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Post_Should_ReturnNewStudentAndStatusCodeCreated()
    {
        // Arrange
        var command = new AddStudentCommand()
        {
            FirstName = "Tomasz",
            LastName = "Kot",
            Email = "t.kot@gmail.com",
            DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
            YearEnrolled = 2022,
        };

        var jsonString = JsonConvert.SerializeObject(command);
        var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/students", stringContent);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StudentDto>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Should().NotBeNull();
        result.Should().BeOfType<StudentDto>();

        Assert.Equal(result.FirstName, command.FirstName);

        Assert.Equal(result.LastName, command.LastName);

        Assert.Equal(result.Email, command.Email);

        int studentAge = (DateTime.Now.Year - command.DateOfBirth.
            ToDateTime(TimeOnly.Parse("00:00")).Year);
        result.Age.Should().Be(studentAge);

        Assert.Equal(result.YearEnrolled, command.YearEnrolled);
    }

    [Fact]
    public async Task Delete_Should_ReturnStatusCodeNoContent()
    {
        // Arrange
        var scopeFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<GradebookDbContext>();

        var student = _dbContext.Students.Add(
            new Student()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "j.kowalski@gmail.com",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
                YearEnrolled = 2022,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

        await _dbContext.SaveChangesAsync();

        int studentId = student.Entity.Id;

        // Act
        var response = await _httpClient.DeleteAsync($"/api/students/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
