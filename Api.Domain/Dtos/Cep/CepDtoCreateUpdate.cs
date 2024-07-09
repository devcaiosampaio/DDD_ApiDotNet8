﻿
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep;

public class CepDtoCreateUpdate
{
    [Required(ErrorMessage = "Cep é campo obrigatório.")]
    public string Cep { get; set; } = string.Empty;

    [Required(ErrorMessage = "Logradouro é campo obrigatório.")]
    public string Logradouro { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    [Required(ErrorMessage = "Municipio é campo obrigatório.")]
    public Guid MunicipioId { get; set; }
}

