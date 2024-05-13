using Api.Domain.Entities;

namespace Api.Domain.Interfaces.User.Services;
public interface ILoginService
{
    Task<object?> FindByEmail(UserEntity user);
}
