using Api.Domain.Dtos;

namespace Api.Domain.Interfaces.User.Services;
public interface ILoginService
{
    Task<object?> FindByEmail(LoginDto loginDto);
}
