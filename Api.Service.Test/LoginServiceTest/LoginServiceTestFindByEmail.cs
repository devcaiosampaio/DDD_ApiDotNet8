using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User.Repositories;
using Api.Domain.Security;
using Api.Service.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace Api.Service.Test.LoginServiceTest;
public class LoginServiceTestFindByEmail
{
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly LoginService _loginService;
    private readonly Mock<IOptions<TokenConfiguration>> _tokenConfigurationMock;

    public LoginServiceTestFindByEmail()
    {
        _signingConfigurations = new SigningConfigurations();
        _repositoryMock = new Mock<IUserRepository>();
        var tokenConfiguration = new TokenConfiguration()
        {
            Audience = "ExemploAudience",
            Issuer = "ExemploIssuer",
            Seconds = 120
        };
        _tokenConfigurationMock = new Mock<IOptions<TokenConfiguration>>();
        _tokenConfigurationMock.Setup(x => x.Value).Returns(tokenConfiguration);

        _loginService = new LoginService(_repositoryMock.Object, _signingConfigurations, _tokenConfigurationMock.Object);
    }
    [Fact]
    public async Task FindByEmail_ShouldReturnAuthenticationFailed_WhenLoginDtoIsNull()
    {
        // Act
        object? result = await _loginService.FindByEmail(null);

        // Assert
        Assert.NotNull(result!);
        Assert.False((bool)result!.GetType().GetProperty("authenticated").GetValue(result!));
        Assert.Equal("Falha ao autenticar", result!.GetType()
            .GetProperty("message")
            .GetValue(result));
    }

    [Fact]
    public async Task FindByEmail_ShouldReturnAuthenticationFailed_WhenEmailIsNullOrEmpty()
    {
        // Arrange
        var loginDto = new LoginDto { Email = null };

        // Act
        var result = await _loginService.FindByEmail(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.False((bool)result.GetType().GetProperty("authenticated").GetValue(result));
        Assert.Equal("Falha ao autenticar", result
            .GetType()
            .GetProperty("message")
            .GetValue(result));
    }

    [Fact]
    public async Task FindByEmail_ShouldReturnAuthenticationFailed_WhenUserNotFound()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@example.com" };

        _repositoryMock.Setup(repo => repo.FindByEmail(It.IsAny<string>())).ReturnsAsync((UserEntity)null);

        // Act
        var result = await _loginService.FindByEmail(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.False((bool)result.GetType().GetProperty("authenticated").GetValue(result));
        Assert.Equal("Falha ao autenticar", result
            .GetType()
            .GetProperty("message")
            .GetValue(result));
    }

    [Fact]
    public async Task FindByEmail_ShouldReturnSuccessObject_WhenUserIsFound()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@example.com" };
        var userEntity = new UserEntity { Email = "test@example.com", Name = "Test User" };

        _repositoryMock.Setup(repo => repo.FindByEmail(It.IsAny<string>())).ReturnsAsync(userEntity);

        // Act
        var result = await _loginService.FindByEmail(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.True((bool)result.GetType().GetProperty("authenticated").GetValue(result));
        Assert.Equal("test@example.com", result.GetType().GetProperty("userName").GetValue(result));
        Assert.Equal("Test User", result.GetType().GetProperty("name").GetValue(result));
        Assert.Equal("Usuário Logado com sucesso", result.GetType().GetProperty("message").GetValue(result));
    }
}
