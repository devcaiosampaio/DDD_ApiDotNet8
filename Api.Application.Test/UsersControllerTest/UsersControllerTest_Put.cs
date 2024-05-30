using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.User.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Api.Application.Test.UsersControllerTest;
public class UsersControllerTest_Put
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UsersController _usersController;

    public UsersControllerTest_Put()
    {
        _userServiceMock = new Mock<IUserService>();
        _usersController = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task Put_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var userDtoUpdate = new UserDtoCreateUpdate
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email()
        };

        var userDtoUpdateResult = new UserDtoUpdateResult
        {
            Id = Guid.NewGuid(),
            Name = userDtoUpdate.Name,
            Email = userDtoUpdate.Email,
            UpdateAt = DateTime.UtcNow
        };

        var userId = Guid.NewGuid();

        _userServiceMock
            .Setup(service => service.Put(It.IsAny<UserDtoCreateUpdate>(), It.IsAny<Guid>()))
            .ReturnsAsync(userDtoUpdateResult);

        var result = await _usersController.Put(userId, userDtoUpdate);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        okResult.Value.Should().BeEquivalentTo(userDtoUpdateResult);

        _userServiceMock.Verify(service => service.Put(It.IsAny<UserDtoCreateUpdate>(), It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Put_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _usersController.ModelState.AddModelError("Email", "Required");

        var userDtoCreate = new UserDtoCreateUpdate
        {
            Name = "Invalid User"
            // Email is missing to simulate invalid model state
        };

        var userId = Guid.NewGuid();
        // Act
        var result = await _usersController.Put(userId, userDtoCreate);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().BeAssignableTo<SerializableError>();

        _userServiceMock.Verify(service => service.Put(It.IsAny<UserDtoCreateUpdate>(), It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task Put_ServiceThrowsArgumentException_ReturnsInternalServerError()
    {
        // Arrange
        var userDtoCreate = new UserDtoCreateUpdate
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email()
        };

        _userServiceMock.Setup(service => service.Put(It.IsAny<UserDtoCreateUpdate>(), It.IsAny<Guid>()))
            .ThrowsAsync(new ArgumentException("Error message"));

        // Act
        var result = await _usersController.Put(Guid.NewGuid(), userDtoCreate);

        // Assert
        var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
        statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        statusCodeResult.Value.Should().Be("Error message");

        _userServiceMock.Verify(service => service.Put(It.IsAny<UserDtoCreateUpdate>(), It.IsAny<Guid>()), Times.Once);
    }
}
