
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities;

public class CepEntity : BaseEntity
{
    [Required]
    [MaxLength(10)]
    public string Cep { get; set; } = string.Empty;

    [Required]
    [MaxLength(60)]
    public string Logradouro { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Numero { get; set; } = string.Empty;

    public Guid MunicipioId { get; set; }

    [Required]
    public MunicipioEntity? Municipio { get; set; }
}

