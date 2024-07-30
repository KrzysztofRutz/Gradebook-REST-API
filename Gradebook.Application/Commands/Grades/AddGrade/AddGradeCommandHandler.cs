using AutoMapper;
using Gradebook.Application.Configuration.Commands;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Domain.Exceptions;

namespace Gradebook.Application.Commands.Grades.AddGrade;

internal class AddGradeCommandHandler : ICommandHandler<AddGradeCommand, GradeDto>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddGradeCommandHandler(IGradeRepository gradeRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GradeDto> Handle(AddGradeCommand request, CancellationToken cancellationToken)
    {
        bool isAlreadyExist = await _gradeRepository.IsAlreadyExistAsync(request.Name, request.Value, cancellationToken);
        if (isAlreadyExist)
        {
            throw new GradeAlreadyExistsException(request.Name, request.Value);
        }

        var newGrade = new Grade()
        {
            Name = request.Name,
            Value = request.Value
        };

        _gradeRepository.Add(newGrade);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var gradeDto = _mapper.Map<GradeDto>(newGrade);
        return gradeDto;
        
    }
}
