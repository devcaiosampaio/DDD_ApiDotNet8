
namespace Api.Domain.Models;

public class CepModel
{
    public string Cep { get; set; } = string.Empty;

    public string Logradouro { get; set; } = string.Empty;

	private string _numero = "S/N";

	public string Numero
	{
		get { return _numero; }
		set { _numero = string.IsNullOrEmpty(value)? "S/N" : value; }
	}

}

