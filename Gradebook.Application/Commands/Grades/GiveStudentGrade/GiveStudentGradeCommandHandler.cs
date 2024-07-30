using Gradebook.Application.Configuration.Commands;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Exceptions;

namespace Gradebook.Application.Commands.Grades.GiveStudentGrade;

internal class GiveStudentGradeCommandHandler : ICommandHandler<GiveStudentGradeCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GiveStudentGradeCommandHandler(IStudentRepository studentRepository, 
        IGradeRepository gradeRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _gradeRepository = gradeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(GiveStudentGradeCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (student == null) 
        {
            throw new StudentNotFoundException(request.Id);
        }

        var grade = await _gradeRepository.GetByIdAsync(request.GradeId, cancellationToken);
        if (grade == null)
        {
            throw new GradeNotFoundException(request.GradeId);
        }

        student.GradeId = grade.Id;
        student.Grade = grade;

        _studentRepository.Update(student);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
