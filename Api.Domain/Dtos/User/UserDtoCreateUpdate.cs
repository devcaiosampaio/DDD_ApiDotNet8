using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User;
public class UserDtoCreateUpdate
{
    [Required(ErrorMessage = "Nome é campo obrigatório.")]
    [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é campo obrigatório.")]
    [EmailAddress(ErrorMessage = "Email em formato inválido.")]
    [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
    public string Email { get; set; } = string.Empty;
}
