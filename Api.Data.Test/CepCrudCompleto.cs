
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;

public class CepCrudCompleto(BaseTestData dbTeste) : IClassFixture<BaseTestData>
{
    private readonly MyContext _context = dbTeste.ServiceProvider.GetService<MyContext>()!;

    [Fact]
    public async Task AddCep_MunicipioAndCepValid_ReturnsCepCreated()
    {
        // Arrange
        MunicipioImplementation _repositoryMunicipio = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repositoryMunicipio.InsertAsync(city);
        cityCreated.Should().NotBeNull();
        cityCreated.Id.Should().NotBe(Guid.Empty);
        cityCreated.Nome.Should().Be(city.Nome);
        cityCreated.CodIBGE.Should().Be(city.CodIBGE);
        cityCreated.UfId.Should().Be(city.UfId);

        CepImplementation _repositoryCep = new(_context);

        var cep = new CepEntity
        { 
            Cep = "13.481-001",
            Logradouro = Faker.Address.StreetAddress(), 
            Numero = "0 até 2000",
            MunicipioId = cityCreated.Id
        };
        //Act
        var cepCreated = await _repositoryCep.InsertAsync(cep);

        //Assert
        cepCreated.Should().NotBeNull();
        cepCreated.Id.Should().NotBe(Guid.Empty);
        cepCreated.Cep.Should().Be(cep.Cep);
        cepCreated.Logradouro.Should().Be(cep.Logradouro);
        cepCreated.Numero.Should().Be(cep.Numero);
        cepCreated.MunicipioId.Should().Be(cep.MunicipioId);
    }

    [Fact]
    public async Task UpdateCep_MunicipioAndCepValid_ReturnsCepCreated()
    {
        // Arrange
        MunicipioImplementation _repositoryMunicipio = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repositoryMunicipio.InsertAsync(city);
        cityCreated.Should().NotBeNull();
        cityCreated.Id.Should().NotBe(Guid.Empty);
        cityCreated.Nome.Should().Be(city.Nome);
        cityCreated.CodIBGE.Should().Be(city.CodIBGE);
        cityCreated.UfId.Should().Be(city.UfId);

        CepImplementation _repositoryCep = new(_context);

        var cep = new CepEntity
        {
            Cep = "13.481-001",
            Logradouro = Faker.Address.StreetAddress(),
            Numero = "0 até 2000",
            MunicipioId = cityCreated.Id
        };  
        
        var cepCreated = await _repositoryCep.InsertAsync(cep);
        //Act
        var updatedLogradouro = Faker.Address.StreetAddress();
        cep.Logradouro = updatedLogradouro;
        await _repositoryCep.UpdateAsync(cep);
        var cepUpdated= await _repositoryCep.GetByIdAsync(cep.Id);

        //Assert
        cepUpdated.Should().NotBeNull();
        cepUpdated.Cep.Should().Be(cepCreated.Cep);
        cepUpdated.Logradouro.Should().Be(updatedLogradouro);
        cepUpdated.Numero.Should().Be(cepCreated.Numero);
        cepUpdated.MunicipioId.Should().Be(cepCreated.MunicipioId);
    }
    [Fact]
    public async Task Delete_CepExists_CepIsDeleted()
    {
        // Arrange
        MunicipioImplementation _repositoryMunicipio = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repositoryMunicipio.InsertAsync(city);
        cityCreated.Should().NotBeNull();
        cityCreated.Id.Should().NotBe(Guid.Empty);
        cityCreated.Nome.Should().Be(city.Nome);
        cityCreated.CodIBGE.Should().Be(city.CodIBGE);
        cityCreated.UfId.Should().Be(city.UfId);

        CepImplementation _repositoryCep = new(_context);

        var cep = new CepEntity
        {
            Cep = "13.481-001",
            Logradouro = Faker.Address.StreetAddress(),
            Numero = "0 até 2000",
            MunicipioId = cityCreated.Id
        };

        var cepCreated = await _repositoryCep.InsertAsync(cep);

        // Act        
        bool result = await _repositoryCep.DeleteAsync(cepCreated.Id);
        var deletedCep = await _repositoryCep.GetByIdAsync(cepCreated.Id);

        // Assert
        result.Should().BeTrue();
        deletedCep.Should().BeNull();

    }
    [Fact]
    public async Task CheckifExists_CepExists_ExistsIsTrue()
    {
        // Arrange
        MunicipioImplementation _repositoryMunicipio = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repositoryMunicipio.InsertAsync(city);
        cityCreated.Should().NotBeNull();
        cityCreated.Id.Should().NotBe(Guid.Empty);
        cityCreated.Nome.Should().Be(city.Nome);
        cityCreated.CodIBGE.Should().Be(city.CodIBGE);
        cityCreated.UfId.Should().Be(city.UfId);

        CepImplementation _repositoryCep = new(_context);

        var cep = new CepEntity
        {
            Cep = "13.481-001",
            Logradouro = Faker.Address.StreetAddress(),
            Numero = "0 até 2000",
            MunicipioId = cityCreated.Id
        };

        var cepCreated = await _repositoryCep.InsertAsync(cep);

        // Act        
        bool result = await _repositoryCep.ExistsAsync(cepCreated.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GetById_CepExists_ReturnsCep()
    {
        // Arrange
        MunicipioImplementation _repositoryMunicipio = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repositoryMunicipio.InsertAsync(city);
        cityCreated.Should().NotBeNull();
        cityCreated.Id.Should().NotBe(Guid.Empty);
        cityCreated.Nome.Should().Be(city.Nome);
        cityCreated.CodIBGE.Should().Be(city.CodIBGE);
        cityCreated.UfId.Should().Be(city.UfId);

        CepImplementation _repositoryCep = new(_context);

        var cep = new CepEntity
        {
            Cep = "13.481-001",
            Logradouro = Faker.Address.StreetAddress(),
            Numero = "0 até 2000",
            MunicipioId = cityCreated.Id
        };

        var cepCreated = await _repositoryCep.InsertAsync(cep);       

        // Act        
        var cepGetById = await _repositoryCep.GetByIdAsync(cepCreated.Id);

        // Assert
        cepGetById.Should().NotBeNull();
        cepGetById.Id.Should().NotBe(Guid.Empty);
        cepGetById.Cep.Should().Be(cepCreated.Cep);
        cepGetById.Logradouro.Should().Be(cepCreated.Logradouro);
        cepGetById.Numero.Should().Be(cepCreated.Numero);
    }

    [Fact]
    public async Task GetByCep_CepExists_ReturnsCep()
    {
        // Arrange
        MunicipioImplementation _repositoryMunicipio = new(_context);
        var city = new MunicipioEntity
        {
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(100000, 9999999),
            UfId = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
        };
        var cityCreated = await _repositoryMunicipio.InsertAsync(city);
        cityCreated.Should().NotBeNull();
        cityCreated.Id.Should().NotBe(Guid.Empty);
        cityCreated.Nome.Should().Be(city.Nome);
        cityCreated.CodIBGE.Should().Be(city.CodIBGE);
        cityCreated.UfId.Should().Be(city.UfId);

        CepImplementation _repositoryCep = new(_context);

        var cep = new CepEntity
        {
            Cep = "13.481-001",
            Logradouro = Faker.Address.StreetAddress(),
            Numero = "0 até 2000",
            MunicipioId = cityCreated.Id
        };

        var cepCreated = await _repositoryCep.InsertAsync(cep);

        // Act        
        var cepGetById = await _repositoryCep.GetByCep(cepCreated.Cep);

        // Assert
        cepGetById.Should().NotBeNull();
        cepGetById.Id.Should().NotBe(Guid.Empty);
        cepGetById.Cep.Should().Be(cepCreated.Cep);
        cepGetById.Logradouro.Should().Be(cepCreated.Logradouro);
        cepGetById.Numero.Should().Be(cepCreated.Numero);
        cepGetById.Municipio.Should().NotBeNull();
        cepGetById.Municipio.Uf.Should().NotBeNull();
    }
}

