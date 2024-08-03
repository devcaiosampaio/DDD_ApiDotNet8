
namespace Api.Domain.Dtos.Uf;

public record struct UfDto
{
    public Guid Id { get; set; }
    public string Sigla { get; set; }
    public string Nome { get; set; }
}

