using AutoMapper;
using Api.CrossCutting.Mappings;
namespace Api.Service.Test;

public abstract class BaseServiceTest
{
    public IMapper Mapper;
    protected BaseServiceTest()
    {
        Mapper = MapperGenerator.GetMapper();
    }
}