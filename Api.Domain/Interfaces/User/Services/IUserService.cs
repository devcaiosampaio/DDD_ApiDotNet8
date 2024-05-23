using Api.Domain.Dtos.User;
namespace Api.Domain.Interfaces.User.Services;
public interface IUserService
{
    Task<UserDto?> Get(Guid id);
    Task<IEnumerable<UserDto>?> GetAll();
    Task<UserDtoCreateResult> Post(UserDtoCreateUpdate user);
    Task<UserDtoUpdateResult?> Put(UserDtoCreateUpdate user, Guid id);
    Task<bool> Delete(Guid id);
}

