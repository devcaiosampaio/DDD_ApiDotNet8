using Api.Domain.Entities;

namespace Api.Domain.Interfaces.User.Repositories;
public interface IUserRepository : IRepository<UserEntity>
{
    public Task<UserEntity?> FindByEmail(string email);
}
