using AutoMapper;
using Gradebook.Application.Configuration.Mappings;
using Gradebook.Domain.Abstractions;
using Moq;

namespace Gradebook.UnitTests.Commands.Grades.AddGrade;

public class AddGradeCommandHandlerTests
{
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;

    public AddGradeCommandHandlerTests()
    {
        _studentRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapper = MapperHelper.CreateMapper(new StudentMappingProfile());
    }
}
