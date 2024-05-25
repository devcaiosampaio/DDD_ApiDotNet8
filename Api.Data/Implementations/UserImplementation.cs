using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations;

public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
{
    private readonly DbSet<UserEntity> _dataset;
    public UserImplementation(MyContext context) : base(context)
    {
        _dataset = context.Set<UserEntity>();
    }
    public async Task<UserEntity?> FindByEmail(string email)
    {
        return await _dataset.FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}
