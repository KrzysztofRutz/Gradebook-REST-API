using AutoMapper;
using Gradebook.Application.Configuration.Queries;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Exceptions;

namespace Gradebook.Application.Queries.Grades.GetGradeStudents;

internal class GetGradeStudentsQueryHandler : IQueryHandler<GetGradeStudentsQuery, IEnumerable<StudentDto>>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IMapper _mapper;

    public GetGradeStudentsQueryHandler(IGradeRepository gradeRepository, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDto>> Handle(GetGradeStudentsQuery request, CancellationToken cancellationToken)
    {
       var grade = await _gradeRepository.GetByIdAsyncWithStudentsAsync(request.Id, cancellationToken);
        if (grade == null) 
        { 
            throw new GradeNotFoundException(request.Id);
        }

        var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(grade.Students);
        return studentsDto;
    }
}
