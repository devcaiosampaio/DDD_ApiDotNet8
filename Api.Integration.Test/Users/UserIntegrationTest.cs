
using Api.Data.Context;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace Api.Integration.Test.Users;
public class UserIntegrationTest : BaseIntegration
{
    [Fact]
    public async Task Post_ValidUser_ReturnsUserDto()
    {
        //Arrange
        UserDto userDto = new()
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email()
        };

        //Act
        var result = await PostJsonAsync("users", userDto);
        var postResult = await result.Content.ReadAsStringAsync();
        var userCreated = JsonSerializer.Deserialize<UserDtoCreateResult>(postResult);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        userCreated.Name.Should().Be(userDto.Name);
        userCreated.Email.Should().Be(userDto.Email);
        userCreated.Id.Should().NotBe(Guid.Empty);

    }

    [Fact]
    public async Task GetAll_ValidUsers_ReturnsUser()
    {
        // Arrange
          // Always have a User Administrador in Database.
        // Act
        var response = await GetAsync($"users");
        var getResult = await response.Content.ReadAsStringAsync();
        var userListRetrieved = JsonSerializer.Deserialize<IEnumerable<UserDto>>(getResult);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        userListRetrieved.Should().NotBeNullOrEmpty();
        userListRetrieved!.Count().Should().NotBe(0);
    }

    [Fact]
    public async Task GetByid_ValidId_ReturnsUser()
    {
        // Arrange
        var userCreated = await CreateUser();

        // Act
        var response = await GetAsync($"users/{userCreated.Id}");
        var getResult = await response.Content.ReadAsStringAsync();
        var userRetrieved = JsonSerializer.Deserialize<UserDto>(getResult);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        userRetrieved.Name.Should().Be(userCreated.Name);
        userRetrieved.Email.Should().Be(userCreated.Email);
    }

    [Fact]
    public async Task Put_ValidId_ReturnsUser()
    {
        // Arrange
        var userCreated = await CreateUser();
        var oldEmail = userCreated.Email;

        userCreated.Email = Faker.Internet.Email();
        // Act
        var response = await PutAsync($"users/{userCreated.Id}", userCreated);
        var getResult = await response.Content.ReadAsStringAsync();
        var userRetrieved = JsonSerializer.Deserialize<UserDtoUpdateResult>(getResult);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        userRetrieved.Name.Should().Be(userCreated.Name);
        userRetrieved.Email.Should().NotBe(oldEmail);
        userRetrieved.Email.Should().Be(userCreated.Email);
        userRetrieved.UpdateAt.Should().NotBe(DateTime.MinValue);

    }

    [Fact]
    public async Task Delete_ValidId_ReturnsTrue()
    {
        // Arrange
        var userCreated = await CreateUser();
        // Act
        var response = await DeleteAsync($"users/{userCreated.Id}");
        var getResult = await response.Content.ReadAsStringAsync();
        var boolRetrieved = Convert.ToBoolean(getResult);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        boolRetrieved.Should().Be(true);

    }
    private async Task<UserDtoCreateResult> CreateUser()
    {
        UserDto userDto = new()
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email()
        };

        //Act
        var result = await PostJsonAsync("users", userDto);
        var postResult = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserDtoCreateResult>(postResult);
    }
}

