
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Api.Data.Test;

public class UfDataTest(BaseTestData dbTeste) : IClassFixture<BaseTestData>
{
    private readonly MyContext _context = dbTeste.ServiceProvider.GetService<MyContext>()!;

    [Fact]
    public async Task TryExistsAsync_UfIdValid_ReturnsTrue()
    {
        // Arrange
        UfImplementation _repository = new(_context);
        Guid validGuidSP = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6");       

        // Act        
        var Ufexists = await _repository.ExistsAsync(validGuidSP);

        // Assert
        Ufexists.Should().BeTrue();
    }

    [Fact]
    public async Task TryExistsAsync_UfIdInvalid_ReturnsFalse()
    {
        // Arrange
        UfImplementation _repository = new(_context);
        Guid invalidGuidSP = new("e7e416de-488c-4fa3-a541-b5af5f35ccf6");

        // Act        
        var Ufexists = await _repository.ExistsAsync(invalidGuidSP);

        // Assert
        Ufexists.Should().BeFalse();
    }

    [Fact]
    public async Task TryGetByIdAsync_UfValid_ReturnsUf()
    {
        // Arrange
        UfImplementation _repository = new(_context);
        Guid validGuidSP = new("e7e416de-477c-4fa3-a541-b5af5f35ccf6");
        UfEntity entity = new()
        {
            Id = validGuidSP,
            Sigla = "SP",
            Nome = "São Paulo"
        };

        // Act        
        var UfSaoPaulo = await _repository.GetByIdAsync(validGuidSP);

        // Assert
        UfSaoPaulo.Should().NotBeNull();
        entity.Sigla.Should().Be(UfSaoPaulo!.Sigla);
        entity.Nome.Should().Be(UfSaoPaulo!.Nome);

    }

    [Fact]
    public async Task TryGetByIdAsync_UfInvalid_ReturnsUf()
    {
        // Arrange
        UfImplementation _repository = new(_context);
        Guid invalidGuidSP = new("e7e416de-488c-4fa3-a541-b5af5f35ccf6");
       
        // Act        
        var UfSaoPaulo = await _repository.GetByIdAsync(invalidGuidSP);

        // Assert
        UfSaoPaulo.Should().BeNull();
    }

}

