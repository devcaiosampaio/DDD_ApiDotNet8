using AutoMapper;
using Api.CrossCutting.Mappings;
namespace Api.Service.Test.UserServiceTest;

public abstract class BaseUserServiceTest
{
    public IMapper Mapper;
    protected BaseUserServiceTest()
    {
        Mapper = MapperGenerator.GetMapper();
    }
}