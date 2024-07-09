using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Municipio;

public class MunicipioDtoCreateUpdate
{
    [Required(ErrorMessage = "Nome de Municipio é campo obrigatório.")]
    [StringLength(60, ErrorMessage = "Nome de Município deve ser no máximo {1} caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Código do IBGE Inválido.")]
    public int CodIBGE { get; set; }

    [Required(ErrorMessage = "Código de UF é campo obrigatório.")]
    public string UfId { get; set; } = string.Empty;

}

