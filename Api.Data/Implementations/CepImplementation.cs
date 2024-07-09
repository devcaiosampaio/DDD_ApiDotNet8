
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Cep.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Api.Data.Implementations;

public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
{
    private readonly DbSet<CepEntity> _dataSet;
    public CepImplementation(MyContext dbContext) : base(dbContext)
    {
        _dataSet = dbContext.Set<CepEntity>();
    }

    public async Task<CepEntity?> GetByCep(string cep)
    {
        return await _dataSet.Include(c => c.Municipio)
            .ThenInclude(m => m.Uf)
            .FirstOrDefaultAsync(c => c.Cep.Equals(cep));
    }
}

