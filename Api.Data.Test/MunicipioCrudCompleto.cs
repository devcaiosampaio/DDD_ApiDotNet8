using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;
public class MunicipioCrudCompleto(BaseTestData dbTeste) : IClassFixture<BaseTestData>
{
    private readonly MyContext _context = dbTeste.ServiceProvider.GetService<MyContext>()!;

    #region Testes relacionados a BaseRepository
    [Fact]
    public async Task Add_MunicipioValid_ReturnsMunicipioCreated()
    {
        // Arrange
        MunicipioImplementation _repository = new(_context);
        var city = new MunicipioEntity 
        {
           Nome = Faker.Address.City(),
           CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
           UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };

        //Act
        var cityCreated = await _repository.InsertAsync(city);

        // Assert
        cityCreated.Should().NotBeNull();
        cityCreated.Id.Should().NotBe(Guid.Empty);
        cityCreated.Nome.Should().Be(city.Nome);
        cityCreated.CodIBGE.Should().Be(city.CodIBGE);
        cityCreated.UfId.Should().Be(city.UfId);
    }   

    [Fact]
    public async Task Update_UserExists_MunicipioIsUpdated()
    {
        // Arrange
        MunicipioImplementation _repository = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repository.InsertAsync(city);

        // Act
        var updateNome = Faker.Address.City();
        city.Nome = updateNome;

        await _repository.UpdateAsync(city);
        var updatedCity = await _repository.GetByIdAsync(city.Id);

        // Assert
        updatedCity.Should().NotBeNull();
        updatedCity!.Id.Should().Be(cityCreated.Id);
        updatedCity.Nome.Should().Be(updateNome);
    }
    [Fact]
    public async Task Delete_MunicipioExists_UserIsDeleted()
    {
        // Arrange
        MunicipioImplementation _repository = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repository.InsertAsync(city);

        // Act        
        bool result = await _repository.DeleteAsync(cityCreated.Id);
        var updatedCity = await _repository.GetByIdAsync(cityCreated.Id);

        // Assert
        result.Should().BeTrue();
        updatedCity.Should().BeNull();

    }
    [Fact]
    public async Task CheckifExists_MunicipioExists_ExistsIsTrue()
    {
        // Arrange
        MunicipioImplementation _repository = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repository.InsertAsync(city);

        // Act        
        bool result = await _repository.ExistsAsync(cityCreated.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GetById_MunicipioExists_ReturnsMunicipio()
    {
        // Arrange
        MunicipioImplementation _repository = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repository.InsertAsync(city);

        // Act        
        var cityGetById = await _repository.GetByIdAsync(cityCreated.Id);

        // Assert
        cityGetById.Should().NotBeNull();
        cityGetById.Id.Should().NotBe(Guid.Empty);
        cityGetById.Nome.Should().Be(city.Nome);
        cityGetById.CodIBGE.Should().Be(city.CodIBGE);
        cityGetById.UfId.Should().Be(city.UfId);
        cityGetById.Uf.Should().BeNull();
    }
    #endregion
    #region Testes Especializado Municipio
    [Fact]
    public async Task GetCompleteByIBGE_CodIBGEExists_ReturnsMunicipioComplete()
    {
        // Arrange
        MunicipioImplementation _repository = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repository.InsertAsync(city);

        // Act        
        var cityGetByCodIBGE = await _repository.GetCompleteByIBGE(cityCreated.CodIBGE);

        // Assert
        cityGetByCodIBGE.Should().NotBeNull();
        cityGetByCodIBGE.Id.Should().NotBe(Guid.Empty);
        cityGetByCodIBGE.Nome.Should().Be(city.Nome);
        cityGetByCodIBGE.CodIBGE.Should().Be(city.CodIBGE);
        cityGetByCodIBGE.UfId.Should().Be(city.UfId);
        cityGetByCodIBGE.Uf.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCompleteById_Exists_ReturnsMunicipioComplete()
    {
        // Arrange
        MunicipioImplementation _repository = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repository.InsertAsync(city);

        // Act        
        var cityGetById = await _repository.GetCompleteById(cityCreated.Id);

        // Assert
        cityGetById.Should().NotBeNull();
        cityGetById.Id.Should().NotBe(Guid.Empty);
        cityGetById.Nome.Should().Be(city.Nome);
        cityGetById.CodIBGE.Should().Be(city.CodIBGE);
        cityGetById.UfId.Should().Be(city.UfId);
        cityGetById.Uf.Should().NotBeNull();
    }
    #endregion

}


