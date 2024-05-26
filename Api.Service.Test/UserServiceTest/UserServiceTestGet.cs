using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User.Services;
using Api.Service.Services;
using Api.Service.Test.Usuario;
using Moq;

namespace Api.Service.Test.UserServiceTest;

public class UserServiceTestGet : BaseTestService
{
    private readonly Mock<IRepository<UserEntity>> _repositoryMock;
    private readonly UserService _userService;

    public UserServiceTestGet()
    {
        _repositoryMock = new Mock<IRepository<UserEntity>>();
        _userService = new UserService(_repositoryMock.Object, _mapper);
    }
    [Fact]
    public async Task GetUserService_IdValid_ReturnsUser()
    {
        //Arrange
        var idUsuario = Guid.NewGuid();
        UserEntity userEntity = new()
        {
            Id = Guid.NewGuid(),
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow

        };
        _repositoryMock
            .Setup(repo => repo.GetByIdAsync(idUsuario))
            .ReturnsAsync(userEntity);

        //Act
        var result = await _userService.Get(idUsuario);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Name, result.Value.Name);
        Assert.Equal(userEntity.Email, result.Value.Email);
        Assert.True(userEntity.Id == result.Value.Id);

    }
    [Fact]
    public async Task GetUserService_IdInvalid_ReturnsNull()
    {
        //Arrange
        _repositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((UserEntity?) null);

        //Act
        var result = await _userService.Get(Guid.NewGuid());

        //Assert
        Assert.True(result.Value.Id == Guid.Empty);
        Assert.True(result.Value.CreateAt == DateTime.MinValue);
        Assert.Null(result.Value.Email);
        Assert.Null(result.Value.Name);
    }
}
