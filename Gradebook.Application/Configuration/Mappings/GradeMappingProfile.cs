using AutoMapper;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Entities;

namespace Gradebook.Application.Configuration.Mappings;

public class GradeMappingProfile : Profile
{
    public GradeMappingProfile()
    {
        CreateMap<Grade, GradeDto>();
    }
}
