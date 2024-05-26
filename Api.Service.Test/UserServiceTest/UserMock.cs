using Api.Domain.Dtos.User;
using System.IO.IsolatedStorage;

namespace Api.Service.Test.Usuario;

public class UserMock
{
    public static Guid IdUsuario { get; set; }
    public static string NomeUsuario { get; set; } = string.Empty;
    public static string EmailUsuario { get; set; } = string.Empty;
    public static string NomeUsuarioAlterado { get; set; } = string.Empty;
    public static string EmailUsuarioAlterado { get; set; } = string.Empty;    

    public List<UserDto> listUserDto = new();

    public UserDto userDto = new();

    public readonly UserDtoCreateUpdate userDtoCreateUpdate;

    public readonly UserDtoCreateResult userDtoCreateResult;

    public readonly UserDtoUpdateResult userDtoUpdateResult;

    public UserMock()
    {
        IdUsuario = Guid.NewGuid();
        NomeUsuario = Faker.Name.FullName();
        EmailUsuario = Faker.Internet.Email();
        NomeUsuarioAlterado = Faker.Name.FullName();
        EmailUsuarioAlterado = Faker.Internet.Email();

        for (int i = 0; i < 10; i++)
        {
            listUserDto.Add(new UserDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            });
        }
        userDto = new() { Id = IdUsuario, Name = NomeUsuario };
        userDtoCreateUpdate = new() { Name = NomeUsuario, Email = EmailUsuario };
        userDtoCreateResult = new() { Id = IdUsuario, Name = NomeUsuario, Email = EmailUsuario, CreateAt = DateTime.UtcNow };
        userDtoUpdateResult = new() { Id = IdUsuario, Name = NomeUsuario, Email = EmailUsuario, UpdateAt = DateTime.UtcNow };
    }

}
