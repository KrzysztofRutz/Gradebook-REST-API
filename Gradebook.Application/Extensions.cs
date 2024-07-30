using FluentValidation;
using Gradebook.Application.Commands.Students.AddStudent;
using Gradebook.Application.Commands.Students.UpdateStudent;
using Gradebook.Application.Configuration.Validation;
using Gradebook.Application.Middlewares;
using Gradebook.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gradebook.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(executingAssembly));
        services.AddAutoMapper(executingAssembly);

        services.AddScoped<IValidator<AddStudentCommand>, AddStudentCommandValidation>();
        services.AddScoped<IValidator<UpdateStudentCommand>, UpdateStudentCommandValidation>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
    }

    public static IApplicationBuilder UseApllication(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}
