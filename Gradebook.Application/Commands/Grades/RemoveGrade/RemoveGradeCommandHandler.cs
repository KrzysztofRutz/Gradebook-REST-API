using Gradebook.Application.Configuration.Commands;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gradebook.Application.Commands.Grades.RemoveGrade;

internal class RemoveGradeCommandHandler : ICommandHandler<RemoveGradeCommand>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveGradeCommandHandler> _logger;

    public RemoveGradeCommandHandler(IGradeRepository gradeRepository, IUnitOfWork unitOfWork, ILogger<RemoveGradeCommandHandler> logger)
    {
        _gradeRepository = gradeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task Handle(RemoveGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (grade == null)
        { 
            throw new GradeNotFoundException(request.Id);
        }

        _gradeRepository.Delete(grade);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogDebug($"Grade with ID {request.Id} was removed.");
    }
}
