using Gradebook.Application.Configuration.Commands;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Gradebook.Application.Commands.Students.RemoveStudent;

internal class RemoveStudentCommandHandler : ICommandHandler<RemoveStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveStudentCommandHandler> _logger;

    public RemoveStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork, ILogger<RemoveStudentCommandHandler> logger)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (student == null)
        {
            throw new StudentNotFoundException(request.Id);
        }

        _studentRepository.Delete(student);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogDebug($"Student with ID {request.Id} was removed.");
    }
}
