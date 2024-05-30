using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Models;
using Api.Service.Services;
using FluentAssertions;
using Moq;

namespace Api.Service.Test.UserServiceTest;

public class UserServiceTestGetAll : BaseUserServiceTest
{
    private readonly Mock<IRepository<UserEntity>> _repositoryMock;
    private readonly UserService _userService;

    public UserServiceTestGetAll()
    {
        _repositoryMock = new Mock<IRepository<UserEntity>>();
        _userService = new UserService(_repositoryMock.Object, Mapper);
    }
    [Fact]
    public async Task GetAllUserService_HaveTwoUsers_ReturnsListOfTwoUsers()
    {
        //Arrange
        IEnumerable<UserEntity> userEntities = new List<UserEntity>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow

            },
            new ()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }
        }; 
        _repositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(userEntities);

        var userModels = Mapper.Map<IEnumerable<UserModel>>(userEntities);
        var userDtos = Mapper.Map<IEnumerable<UserDto>>(userModels);
        
        //Act
        var result = await _userService.GetAll();

        //Assert

        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(userDtos, options => options.WithStrictOrdering());

    }
    [Fact]
    public async Task GetAllUserService_DontHaveUser_ReturnsAnEmptyList()
    {
        //Arrange
        IEnumerable<UserEntity> userEntities = [];
        _repositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(userEntities);

        var userModels = Mapper.Map<IEnumerable<UserModel>>(userEntities);
        var userDtos = Mapper.Map<IEnumerable<UserDto>>(userModels);

        //Act
        var result = await _userService.GetAll();

        //Assert
        result.Should().BeEmpty();
        result.Should().BeEquivalentTo(userDtos, options => options.WithStrictOrdering());


    }
}
