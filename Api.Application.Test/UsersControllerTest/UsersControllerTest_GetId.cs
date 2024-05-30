using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.User.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Api.Application.Test.UsersControllerTest;
public class UsersControllerTest_GetId
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UsersController _usersController;

    public UsersControllerTest_GetId()
    {
        _userServiceMock = new Mock<IUserService>();
        _usersController = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetId_ValidId_ReturnsOkResult()
    {
        //Arrange
        var userId = Guid.NewGuid();
        UserDto? userReturned = new ()
        { 
            Id = userId,
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            CreateAt = DateTime.UtcNow
        };
        _userServiceMock
            .Setup(service => service.Get(It.IsAny<Guid>()))
            .Returns(Task.FromResult(userReturned));

        //Act
        var result = await _usersController.GetId(userId);

        //Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(userReturned);
        _userServiceMock.Verify(service => service.Get(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task GetId_InvalidModel_ReturnsBadRequestt()
    {
        //Arrange
        _usersController.ModelState.AddModelError("Email", "Required");
        var userId = Guid.NewGuid();
        //Act
        var result = await _usersController.GetId(userId);
        //Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().BeAssignableTo<SerializableError>();

        _userServiceMock.Verify(service => service.Get(It.IsAny<Guid>()), Times.Never);

    }

    [Fact]
    public async Task GetId_ServiceThrowsArgumentException_ReturnsInternalServerError()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userServiceMock.Setup(service => service.Get(It.IsAny<Guid>()))
            .ThrowsAsync(new ArgumentException("Error message"));

        // Act
        var result = await _usersController.GetId(userId);

        // Assert
        var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
        statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        statusCodeResult.Value.Should().Be("Error message");

        _userServiceMock.Verify(service => service.Get(It.IsAny<Guid>()), Times.Once);
    }
}
