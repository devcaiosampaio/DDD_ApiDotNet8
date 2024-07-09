using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User.Services;
using Api.Service.Services.User;
using Api.Service.Test.Usuario;
using Moq;

namespace Api.Service.Test.UserServiceTest
{
    public class UserServiceTestPut : BaseUserServiceTest
    {
        private readonly Mock<IRepository<UserEntity>> _repositoryMock;
        private readonly UserService _userService;

        public UserServiceTestPut()
        {
            _repositoryMock = new Mock<IRepository<UserEntity>>();
            _userService = new UserService(_repositoryMock.Object, Mapper);
        }
        [Fact]
        public async Task PutUserService_UserValid_ReturnsUserWithNewName()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            UserEntity user = new()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow

            };
            _repositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(user);

            UserDtoCreateUpdate userToUpdate = new()
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };
            //Act

            var resultUpdate = await _userService.Put(userToUpdate, Guid.NewGuid());

            Assert.NotNull(resultUpdate);
            Assert.Equal(resultUpdate.Value.Name, user.Name);
            Assert.Equal(resultUpdate.Value.Email, user.Email);
            Assert.NotEqual(resultUpdate.Value.UpdateAt, DateTime.MinValue);


        }
    }
}
