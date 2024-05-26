using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;
public class UsuarioCrudCompleto(BaseTestData dbTeste) : IClassFixture<BaseTestData>
{
    private readonly MyContext _context = dbTeste.ServiceProvider.GetService<MyContext>()!;

    [Fact]
    public async Task TryGetByLogin_UserExists_ReturnsUser()
    {
        // Arrange
        UserImplementation _repository = new(_context);
        var user = new UserEntity { Email = "ut_login@teste.com", Name = "ut_login" };
        await _repository.InsertAsync(user);

        // Act        
        var userGotByLogin = await _repository.FindByEmail(user.Email);

        // Assert
        Assert.NotNull(userGotByLogin);
        Assert.Equal("ut_login@teste.com", userGotByLogin.Email);
        Assert.Equal("ut_login", userGotByLogin.Name);
    }

    [Fact]
    public async Task AddUser_EmailAndNameIsValid_UserIsAddedToDatabase()
    {
        //Arrange
        UserImplementation _repository = new(_context);
        UserEntity userEntity = new()
        {
            Email = "ut_adduser@teste.com",
            Name = "ut_adduser"
        };
        //Act
        await _repository.InsertAsync(userEntity);
        var userGotbyId = await _repository.GetByIdAsync(userEntity.Id);

        //Assert
        Assert.NotNull(userGotbyId);
        Assert.Equal("ut_adduser@teste.com", userGotbyId.Email);
        Assert.Equal("ut_adduser", userGotbyId.Name);

    }

    [Fact]
    public async Task UpdateUser_UserExists_UserIsUpdated()
    {
        // Arrange
        UserImplementation _repository = new(_context);
        var user = new UserEntity { Email = "ut_updateuser@teste.com", Name = "ut_updateuser" };
        await _repository.InsertAsync(user);

        // Act        
        user.Name = "updated_name";
        await _repository.UpdateAsync(user);
        var updatedUser = await _repository.GetByIdAsync(user.Id);

        // Assert
        Assert.NotNull(updatedUser);
        Assert.Equal("updated_name", updatedUser.Name);
    }
    [Fact]
    public async Task DeleteUser_UserExists_UserIsDeleted()
    {
        // Arrange
        UserImplementation _repository = new(_context);
        var user = new UserEntity { Email = "ut_deleteuser@teste.com", Name = "ut_deleteuser" };
        await _repository.InsertAsync(user);

        // Act        
        bool result = await _repository.DeleteAsync(user.Id);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public async Task CheckifUserExists_UserExists_ExistsIsTrue()
    {
        // Arrange
        UserImplementation _repository = new(_context);
        var user = new UserEntity { Email = "ut_existsuser@teste.com", Name = "ut_existsuser" };
        await _repository.InsertAsync(user);

        // Act        
        bool result = await _repository.ExistsAsync(user.Id);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public async Task TryGetAllUser_UserExists_GetAllUsers()
    {
        // Arrange
        UserImplementation _repository = new(_context);
        var user = new UserEntity { Email = "ut_getall@teste.com", Name = "ut_getall" };
        await _repository.InsertAsync(user);

        // Act        
        var getAll = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(getAll);
        Assert.True(getAll.Any());
    }

}
