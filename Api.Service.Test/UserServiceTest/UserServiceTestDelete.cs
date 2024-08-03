using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Service.Services.User;
using FluentAssertions;
using Moq;

namespace Api.Service.Test.UserServiceTest;

public class UserServiceTestDelete : BaseServiceTest
{
    private readonly Mock<IRepository<UserEntity>> _repositoryMock;
    private readonly UserService _userService;

    public UserServiceTestDelete()
    {
        _repositoryMock = new Mock<IRepository<UserEntity>>();
        _userService = new UserService(_repositoryMock.Object, Mapper);
    }
    [Fact]
    public async Task DeleteUserService_IdValid_ReturnsTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _repositoryMock
            .Setup(repo => repo.DeleteAsync(userId))
            .ReturnsAsync(true);

        // Act
        var result = await _userService.Delete(userId);

        // Assert
        result.Should().BeTrue();
        _repositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);

    }
    [Fact]
    public async Task DeleteUserService_IdInvalid_ReturnsFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _repositoryMock
            .Setup(repo => repo.DeleteAsync(userId))
            .ReturnsAsync(false);

        // Act
        var result = await _userService.Delete(userId);

        // Assert
        result.Should().BeFalse();
        _repositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
    }
}
