using AutoMapper;

namespace Gradebook.UnitTests;

internal class MapperHelper
{
    public static IMapper CreateMapper(Profile profile)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(profile);
        });

        return mappingConfig.CreateMapper();    
    }
}
