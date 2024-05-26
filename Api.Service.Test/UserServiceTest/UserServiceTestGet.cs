using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.User.Services;
using Api.Service.Test.Usuario;
using Moq;

namespace Api.Service.Test.UserServiceTest;

public class UserServiceTestGet : UserMock
{
    private IUserService _userService;
    private Mock<IUserService> _userServiceMock;

    [Fact]
    public async Task GetUserService_IdValid_ReturnsUser()
    {
        //Arrange
        _userServiceMock = new Mock<IUserService>();
        _userServiceMock
            .Setup(mock => mock.Get(IdUsuario))
            .ReturnsAsync(userDto);
        _userService = _userServiceMock.Object;

        //Act
        var result = await _userService.Get(IdUsuario);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Name, result.Value.Name);
        Assert.True(userDto.Id == result.Value.Id);

    }
    [Fact]
    public async Task GetUserService_IdInvalid_ReturnsNull()
    {
        //Arrange
        _userServiceMock = new Mock<IUserService>();
        _userServiceMock
            .Setup(mock => mock.Get(It.IsAny<Guid>()))
            .ReturnsAsync((UserDto?) null);
        _userService = _userServiceMock.Object;

        //Act
        var result = await _userService.Get(IdUsuario);

        //Assert
        Assert.Null(result);
    }
}
