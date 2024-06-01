using Api.CrossCutting.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.AppDependencies;
public static class AutoMapperInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddSingleton(MapperGenerator.GetMapper());

        return services;
    }
}
