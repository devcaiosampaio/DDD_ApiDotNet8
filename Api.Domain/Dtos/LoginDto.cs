using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos;
public class LoginDto
{
    [Required(ErrorMessage = "Email é um campo obrigatório para o Login.")]
    [EmailAddress(ErrorMessage = "Email com o formato inválido.")]
    [StringLength(100, ErrorMessage = "Email deve ter no máximo {100} caracteres.")]
    public string Email { get; set; } = string.Empty;
}
