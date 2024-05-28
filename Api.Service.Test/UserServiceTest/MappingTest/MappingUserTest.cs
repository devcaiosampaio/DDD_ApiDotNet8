using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;

namespace Api.Service.Test.UserServiceTest.MappingTest;
public class MappingUserTest: BaseUserServiceTest
{
    [Fact]
    public void MappingUserModelToUserEntity_UserModelValid_ReturnsUserEntityValid()
    {
        //Arrange
        UserModel model = new()
        {
            Id = Guid.NewGuid(),
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            CreateAt = DateTime.UtcNow
        };
        //Act
        var entity = Mapper.Map<UserEntity>(model);

        //Assert

        Assert.NotNull(entity);
        Assert.Equal(model.Id, entity.Id);
        Assert.Equal(model.Name, entity.Name);
        Assert.Equal(model.Email, entity.Email);
        Assert.Equal(model.CreateAt, entity.CreateAt);

    }
    [Fact]
    public void MappingUserDtosToUserModel_UserDtosValids_ReturnsUserDtosValids()
    {
        //Arrange
        UserDto userDto = new()
        {
            Id = Guid.NewGuid(),
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            CreateAt = DateTime.UtcNow
        };
        UserDtoCreateUpdate userDtoCreateUpdate = new()
        {
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email()
        };
        //Act
        var userDtoToModel = Mapper.Map<UserModel>(userDto);
        var userDtoCreateUpdateToModel = Mapper.Map<UserModel>(userDtoCreateUpdate);

        //Assert
        //Dto To UserModel
        Assert.NotNull(userDtoToModel);
        Assert.Equal(userDtoToModel.Id, userDto.Id);
        Assert.Equal(userDtoToModel.Name, userDto.Name);
        Assert.Equal(userDtoToModel.Email, userDto.Email);
        Assert.Equal(userDtoToModel.CreateAt, userDto.CreateAt);

        //DtoCreateUpdate To UserModel
        Assert.NotNull(userDtoCreateUpdateToModel);
        Assert.Equal(userDtoCreateUpdateToModel.Name, userDtoCreateUpdate.Name);
        Assert.Equal(userDtoCreateUpdateToModel.Email, userDtoCreateUpdate.Email);

    }
    [Fact]
    public void MappingUserEntityToUserDtos_UserEntityValid_ReturnsUserDtosValids()
    {
        //Arrange
        UserEntity entity = new()
        {
            Id = Guid.NewGuid(),
            Name = Faker.Name.FullName(),
            Email = Faker.Internet.Email(),
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow.AddDays(1),
        };
        //Act
        var entityToDto = Mapper.Map<UserDto>(entity);
        var entityToDtoCreateResult = Mapper.Map<UserDtoCreateResult>(entity);
        var entityToDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(entity);

        //Assert
        //entityToDto
        Assert.Equal(entityToDto.Id, entity.Id);
        Assert.Equal(entityToDto.Name, entity.Name);
        Assert.Equal(entityToDto.Email, entity.Email);
        Assert.Equal(entityToDto.CreateAt, entity.CreateAt);

        //entityToDtoCreateResult
        Assert.Equal(entityToDtoCreateResult.Id, entity.Id);
        Assert.Equal(entityToDtoCreateResult.Name, entity.Name);
        Assert.Equal(entityToDtoCreateResult.Email, entity.Email);
        Assert.Equal(entityToDtoCreateResult.CreateAt, entity.CreateAt);
        //entityToDtoUpdateResult
        Assert.Equal(entityToDtoUpdateResult.Id, entity.Id);
        Assert.Equal(entityToDtoUpdateResult.Name, entity.Name);
        Assert.Equal(entityToDtoUpdateResult.Email, entity.Email);
        Assert.Equal(entityToDtoUpdateResult.UpdateAt, entity.UpdateAt);

    }
}
    