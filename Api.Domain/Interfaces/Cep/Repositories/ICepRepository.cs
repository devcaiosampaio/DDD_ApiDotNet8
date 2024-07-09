using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Cep.Repositories;

public interface ICepRepository : IRepository<CepEntity>
{
    Task<CepEntity?> GetByCep(string cep);
}
