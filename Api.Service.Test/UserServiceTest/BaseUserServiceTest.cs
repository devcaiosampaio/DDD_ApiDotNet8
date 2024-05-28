using AutoMapper;
using Api.CrossCutting.Mappings;
namespace Api.Service.Test.UserServiceTest;

public abstract class BaseUserServiceTest
{
    public IMapper Mapper;
    protected BaseUserServiceTest()
    {
        Mapper = new AutoMapperFixture().GetMapper();
    }
}
public sealed class AutoMapperFixture : IDisposable
{
    public IMapper GetMapper()
    {
        return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            }).CreateMapper();
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}