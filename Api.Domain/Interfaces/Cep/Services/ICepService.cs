using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;

namespace Api.Domain.Interfaces.Cep.Services;

public interface ICepService
{

    Task<CepDto> Get(Guid id);

    Task<CepDto> Get(string cep);

    Task<CepDtoCreateResult> Post(CepDtoCreateUpdate cep);

    Task<CepDtoCreateResult> Put(CepDtoCreateUpdate cep, Guid id);

    Task<bool> Delete(Guid id);
}
