using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Municipio.Services
{
    public interface IMunicipioService
    {
        Task<MunicipioDto> Get(Guid id);
        Task<MunicipioDtoCompleto> GetCompletebyid(Guid id);
        Task<MunicipioDtoCompleto> GetCompleteByIBGE(int codIBGE);
        Task<IEnumerable<MunicipioDtoCompleto>> GetAll(Guid id);
        Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreateUpdate municipio);
        Task<MunicipioDtoUpdateResult> Put(MunicipioDtoCreateUpdate municipio, Guid id);
        Task<bool> Delete(Guid id);
    }
}
