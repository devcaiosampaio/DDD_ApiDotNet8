using Api.CrossCutting.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.AppDependencies;
public static class AutoMapperInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DtoToModelProfile());
            cfg.AddProfile(new EntityToDtoProfile());
            cfg.AddProfile(new ModelToEntityProfile());
        });
        services.AddSingleton(config.CreateMapper());

        return services;
    }
}
