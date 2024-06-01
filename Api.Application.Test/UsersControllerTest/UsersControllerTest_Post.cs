using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.User.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Api.Application.Test.UsersControllerTest;
public class UsersControllerTest_Post
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UsersController _usersController;

    public UsersControllerTest_Post()
    {
        _userServiceMock = new Mock<IUserService>();
        _usersController = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task Post_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var userDtoCreate = new UserDtoCreateUpdate
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email()
        };

        var userDtoCreateResult = new UserDtoCreateResult
        {
            Id = Guid.NewGuid(),
            Name = userDtoCreate.Name,
            Email = userDtoCreate.Email,
            CreateAt = DateTime.UtcNow
        };

        Mock<IUrlHelper> url = new();
        url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:7052");
        _usersController.Url = url.Object;

        _userServiceMock.Setup(service => service.Post(It.IsAny<UserDtoCreateUpdate>()))
            .ReturnsAsync(userDtoCreateResult);

        // Act
        var result = await _usersController.Post(userDtoCreate);

        // Assert
        var okResult = result.Should().BeOfType<CreatedResult>().Subject;

        okResult.Value.Should().BeEquivalentTo(userDtoCreateResult);

        _userServiceMock.Verify(service => service.Post(It.IsAny<UserDtoCreateUpdate>()), Times.Once);
    }

    [Fact]
    public async Task Post_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        _usersController.ModelState.AddModelError("Email", "Required");

        var userDtoCreate = new UserDtoCreateUpdate
        {
            Name = "Invalid User"
            // Email is missing to simulate invalid model state
        };

        // Act
        var result = await _usersController.Post(userDtoCreate);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().BeAssignableTo<SerializableError>();

        _userServiceMock.Verify(service => service.Post(It.IsAny<UserDtoCreateUpdate>()), Times.Never);
    }

    [Fact]
    public async Task Post_ServiceThrowsArgumentException_ReturnsInternalServerError()
    {
        // Arrange
        var userDtoCreate = new UserDtoCreateUpdate
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email()
        };

        _userServiceMock.Setup(service => service.Post(It.IsAny<UserDtoCreateUpdate>()))
            .ThrowsAsync(new ArgumentException("Error message"));

        // Act
        var result = await _usersController.Post(userDtoCreate);

        // Assert
        var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
        statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        statusCodeResult.Value.Should().Be("Error message");

        _userServiceMock.Verify(service => service.Post(It.IsAny<UserDtoCreateUpdate>()), Times.Once);
    }
}