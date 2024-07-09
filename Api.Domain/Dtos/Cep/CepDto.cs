
using Api.Domain.Dtos.Municipio;

namespace Api.Domain.Dtos.Cep;
public class CepDto
{
    public Guid Id { get; set; }

    public string Cep { get; set; } = string.Empty;

    public string Logradouro { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    public Guid MunicipioId { get; set; }
    public MunicipioDtoCompleto Municipio { get; set; }
}

