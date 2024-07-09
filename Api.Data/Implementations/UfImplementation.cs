
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Uf.Repositories;

namespace Api.Data.Implementations;

public class UfImplementation : BaseRepository<UfEntity>, IUfRepository
{
    public UfImplementation(MyContext dbContext) : base(dbContext)
    {
    }
}

