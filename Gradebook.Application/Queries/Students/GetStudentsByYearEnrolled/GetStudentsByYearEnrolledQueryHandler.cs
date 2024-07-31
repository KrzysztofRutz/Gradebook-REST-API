using AutoMapper;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using MediatR;

namespace Gradebook.Application.Queries.Students.GetStudentsByYearEnrolled;

public class GetStudentsByYearEnrolledQueryHandler : IRequestHandler<GetStudentsByYearEnrolledQuery, IEnumerable<StudentDto>>
{
    private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
    private readonly IMapper _mapper;

    public GetStudentsByYearEnrolledQueryHandler(IStudentReadOnlyRepository studentReadOnlyRepository, IMapper mapper)
    {
        _studentReadOnlyRepository = studentReadOnlyRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<StudentDto>> Handle(GetStudentsByYearEnrolledQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentReadOnlyRepository.GetByYearEnrolledAsync(request.YearEnrolled, cancellationToken);

        var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);    

        return studentsDto;
    }
}
