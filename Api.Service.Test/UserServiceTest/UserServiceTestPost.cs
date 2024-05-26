using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User.Services;
using Api.Service.Services;
using Api.Service.Test.Usuario;
using Moq;
using Xunit.Sdk;
namespace Api.Service.Test.UserServiceTest
{
    public class UserServiceTestPost : BaseTestService
    {
        private readonly Mock<IRepository<UserEntity>> _repositoryMock;
        private readonly UserService _userService;

        public UserServiceTestPost()
        {
            _repositoryMock = new Mock<IRepository<UserEntity>>();
            _userService = new UserService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task PostUserService_EmailNameValid_ReturnsUser()
        {
            //Arrange
            UserDtoCreateUpdate userDtoCreate = new()
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
            };
            UserEntity userCreated = new()
            {
                Id = Guid.NewGuid(),
                Name = userDtoCreate.Name,
                Email = userDtoCreate.Email,
                CreateAt = DateTime.UtcNow
            };
            _repositoryMock
                .Setup(repo => repo.InsertAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(userCreated);
            //Act
            var resultCreate = await _userService.Post(userDtoCreate);
            //Assert

            Assert.Equal(userDtoCreate.Name, resultCreate.Name);
            Assert.Equal(userDtoCreate.Email, resultCreate.Email);
            Assert.False(resultCreate.CreateAt == DateTime.MinValue);
        }

        //Deve retornar null, refazer.
        [Fact]
        public async Task PostUserService_WithNameNull_ReturnsNull()
        {
            //Arrange
            UserDtoCreateUpdate userDtoCreate = new()
            {
                Name = null,
                Email = Faker.Internet.Email(),
            };
            UserEntity userCreated = new()
            {
                Id = Guid.NewGuid(),
                Name = null,
                Email = userDtoCreate.Email,
                CreateAt = DateTime.UtcNow
            };
            _repositoryMock
                .Setup(repo => repo.InsertAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(userCreated);
            //Act
            var resultCreate = await _userService.Post(userDtoCreate);
            //Assert

            Assert.Equal(userDtoCreate.Name, resultCreate.Name);
            Assert.Equal(userDtoCreate.Email, resultCreate.Email);
            Assert.False(resultCreate.CreateAt == DateTime.MinValue);

        }
    }
}
