using AutoMapper;
using Api.CrossCutting.Mappings;
namespace Api.Service.Test;

public abstract class BaseTestService
{
    public IMapper _mapper;
    protected BaseTestService()
    {
        _mapper = new AutoMapperFixture().GetMapper();
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