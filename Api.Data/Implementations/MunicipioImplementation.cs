
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Municipio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations;

public class MunicipioImplementation : BaseRepository<MunicipioEntity>, IMunicipioRepository
{
    private readonly DbSet<MunicipioEntity> _dataSet;
    public MunicipioImplementation(MyContext dbContext) : base(dbContext)
    {
        _dataSet = dbContext.Set<MunicipioEntity>();    
    }

    public async Task<MunicipioEntity?> GetCompleteByIBGE(int codIBGE)
    {
        return await _dataSet.Include(m => m.Uf)
            .FirstOrDefaultAsync(m => m.CodIBGE == codIBGE);
    }

    public async Task<MunicipioEntity?> GetCompleteById(Guid id)
    {
        return await _dataSet.Include(m => m.Uf)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}

