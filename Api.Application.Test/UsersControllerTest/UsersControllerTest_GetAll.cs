using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Api.Application.Test.UsersControllerTest;

public class UsersControllerTest_GetAll
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UsersController _usersController;

    public UsersControllerTest_GetAll()
    {
        _userServiceMock = new Mock<IUserService>();
        _usersController = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ValidListUsers_ReturnsOkResult()
    {
        //Arrange
        IEnumerable<UserDto> users = new List<UserDto>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow

            },
            new ()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow
            }
        };

        _userServiceMock
            .Setup(service => service.GetAll())
            .ReturnsAsync(users);

        //Act
        var result = await _usersController.GetAll();

        //Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(users);
        _userServiceMock.Verify(service => service.GetAll(), Times.Once);
    }
    [Fact]
    public async Task GetAll_InvalidModel_ReturnsBadRequestt()
    {
        //Arrange
        _usersController.ModelState.AddModelError("Email", "Required");
        //Act
        var result = await _usersController.GetAll();
        //Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().BeAssignableTo<SerializableError>();

        _userServiceMock.Verify(service => service.GetAll(), Times.Never);

    }

    [Fact]
    public async Task GetAll_ServiceThrowsArgumentException_ReturnsInternalServerError()
    {
        // Arrange
        _userServiceMock.Setup(service => service.GetAll())
            .ThrowsAsync(new ArgumentException("Error message"));

        // Act
        var result = await _usersController.GetAll();

        // Assert
        var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
        statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        statusCodeResult.Value.Should().Be("Error message");

        _userServiceMock.Verify(service => service.GetAll(), Times.Once);
    }
}
