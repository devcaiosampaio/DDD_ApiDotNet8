
namespace Api.Domain.Dtos.Municipio;

public record struct MunicipioDtoUpdateResult
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int CodIBGE { get; set; }
    public Guid UfId { get; set; }
    public DateTime UpdateAt { get; set; }
}

