
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep;

public record struct CepDtoCreateResult
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Cep é campo obrigatório.")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "Logradouro é campo obrigatório.")]
    public string Logradouro { get; set; }

    public string Numero { get; set; }

    [Required(ErrorMessage = "Municipio é campo obrigatório.")]
    public Guid MunicipioId { get; set; }

    public DateTime CreateAt { get; set; }
}

