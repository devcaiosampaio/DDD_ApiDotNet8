
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings;

public class DtoToModelProfile : Profile
{
    public DtoToModelProfile()
    {
        CreateMap<UserModel, UserDto>()
            .ReverseMap();
        CreateMap<UserModel, UserDtoCreateUpdate>()
            .ReverseMap();

        //UF
        CreateMap<UfModel, UfDto>()
           .ReverseMap();
        #region Municipio
        CreateMap<MunicipioModel, UfDto>()
           .ReverseMap();
        CreateMap<MunicipioModel, MunicipioDtoCreateUpdate>()
            .ReverseMap();
        #endregion

        #region Cep
        CreateMap<CepModel, CepDto>()
           .ReverseMap();
        CreateMap<CepModel, CepDtoCreateUpdate>()
            .ReverseMap();
        #endregion
    }
}
