using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.User.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Api.Application.Test.UsersControllerTest;
public class UsersControllerTest_Delete
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UsersController _usersController;

    public UsersControllerTest_Delete()
    {
        _userServiceMock = new Mock<IUserService>();
        _usersController = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsOkResult()
    {
        // Arrange
        _userServiceMock.Setup(service => service.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        var result = await _usersController.Delete(Guid.NewGuid());

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

        okResult.Value.Should().BeEquivalentTo(true);

        _userServiceMock.Verify(service => service.Delete(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Delete_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _usersController.ModelState.AddModelError("Email", "Required");

        // Act
        var result = await _usersController.Delete(Guid.NewGuid());

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().BeAssignableTo<SerializableError>();

        _userServiceMock.Verify(service => service.Delete(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task Delete_ServiceThrowsArgumentException_ReturnsInternalServerError()
    {
        // Arrange
        _userServiceMock.Setup(service => service.Delete(It.IsAny<Guid>()))
            .ThrowsAsync(new ArgumentException("Error message"));

        // Act
        var result = await _usersController.Delete(Guid.NewGuid());

        // Assert
        var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
        statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        statusCodeResult.Value.Should().Be("Error message");

        _userServiceMock.Verify(service => service.Delete(It.IsAny<Guid>()), Times.Once);
    }
}
