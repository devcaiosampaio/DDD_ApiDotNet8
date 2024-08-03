
using Api.Domain.Dtos.Uf;

namespace Api.Domain.Dtos.Municipio;

public record struct MunicipioDtoCompleto
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public int CodIBGE { get; set; }

    public Guid UfId { get; set; }

    public Uf.UfDto Uf { get; set; }

}

