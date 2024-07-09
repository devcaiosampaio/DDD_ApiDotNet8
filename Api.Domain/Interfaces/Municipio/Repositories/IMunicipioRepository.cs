using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Municipio.Repositories;

public interface IMunicipioRepository : IRepository<MunicipioEntity>
{
    Task<MunicipioEntity?> GetCompleteById(Guid id);
    Task<MunicipioEntity?> GetCompleteByIBGE(int codIBGE);
}
