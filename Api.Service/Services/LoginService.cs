using Api.Domain.Entities;
using Api.Domain.Interfaces.User.Repositories;
using Api.Domain.Interfaces.User.Services;

namespace Api.Service.Services;
public class LoginService(IUserRepository _userRepository) : ILoginService
{
    public async Task<object?> FindByEmail(UserEntity user)
    {
        if (user is null || string.IsNullOrWhiteSpace(user.Email))
            return null;

        return await _userRepository.FindByEmail(user.Email);
    }
}
