using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public static class MapperGenerator
    {
        public static IMapper GetMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            }).CreateMapper();
        }
    }
}
