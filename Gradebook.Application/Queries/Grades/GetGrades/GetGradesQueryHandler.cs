using AutoMapper;
using Gradebook.Application.Configuration.Queries;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using MediatR;

namespace Gradebook.Application.Queries.Grades.GetGrades;

public class GetGradesQueryHandler : IRequestHandler<GetGradesQuery, IEnumerable<GradeDto>>
{
    private readonly IGradeReadOnlyRepository _gradeReadOnlyRepository;
    private readonly IMapper _mapper;

    public GetGradesQueryHandler(IGradeReadOnlyRepository gradeReadOnlyRepository , IMapper mapper)
    {
        _gradeReadOnlyRepository = gradeReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GradeDto>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
    {
        var grades = await _gradeReadOnlyRepository.GetAllAsync(cancellationToken);

        var gradesDto = _mapper.Map<IEnumerable<GradeDto>>(grades);

        return gradesDto;
    }
}
