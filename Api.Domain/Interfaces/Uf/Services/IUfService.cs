using Api.Domain.Dtos.Uf;

namespace Api.Domain.Interfaces.Uf.Services;

public interface IUfService
{
    Task<UfDto> Get(Guid id);
    Task<IEnumerable<UfDto>> GetAll();
}
