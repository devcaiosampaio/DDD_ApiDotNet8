using System.ComponentModel.DataAnnotations;

namespace Api.Data.Settings;
public class DatabaseSettings
{
    [Required(ErrorMessage = "A conexão com o banco de dados é obrigatória.")]
    public string Connection { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo de banco de dados é obrigatório.")]
    public string DatabaseType { get; set; } = string.Empty;
}
