using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Service.Services;
using Moq;

namespace Api.Service.Test.UserServiceTest;

public class UserServiceTestDelete : BaseTestService
{
    private readonly Mock<IRepository<UserEntity>> _repositoryMock;
    private readonly UserService _userService;

    public UserServiceTestDelete()
    {
        _repositoryMock = new Mock<IRepository<UserEntity>>();
        _userService = new UserService(_repositoryMock.Object, _mapper);
    }
    [Fact]
    public async Task DeleteUserService_IdValid_ReturnsTrue()
    {
        //Arrange
        Guid IdUsuario = Guid.NewGuid();
        _repositoryMock
            .Setup(repo => repo.DeleteAsync(IdUsuario))
            .Returns(Task.FromResult(true));            

        //Act
        var result = await _userService.Delete(IdUsuario);

        //Assert
        Assert.True(result);

    }
    [Fact]
    public async Task DeleteUserService_IdInvalid_ReturnsFalse()
    {
        //Arrange
        Guid IdUsuario = Guid.NewGuid();
        _repositoryMock
            .Setup(repo => repo.DeleteAsync(IdUsuario))
            .Returns(Task.FromResult(false));

        //Act
        var result = await _userService.Delete(IdUsuario);

        //Assert
        Assert.False(result);
    }
}
