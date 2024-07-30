using AutoMapper;
using Gradebook.Application.Configuration.Queries;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;

namespace Gradebook.Application.Queries.Students.GetStudentsWithDetails;

public class GetStudentsWithDetailsQueryHandler : IQueryHandler<GetStudentsWithDetailsQuery, IEnumerable<StudentDetailsDto>>
{
    private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
    private readonly IMapper _mapper;

    public GetStudentsWithDetailsQueryHandler(IStudentReadOnlyRepository studentReadOnlyRepository, IMapper mapper)
    {
        _studentReadOnlyRepository = studentReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDetailsDto>> Handle(GetStudentsWithDetailsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentReadOnlyRepository.GetAllWithDetailsAsync(cancellationToken);

        var studentsDto = _mapper.Map<IEnumerable<StudentDetailsDto>>(students);

        return studentsDto;
    }
}
