﻿
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities;

public class MunicipioEntity : BaseEntity
{
    [Required]
    [MaxLength(60)]
    public string Nome { get; set; } = string.Empty;
    public int CodIBGE { get; set; }
    [Required]
    public Guid UfId { get; set; }
    [Required]
    public UfEntity Uf { get; set; } = new UfEntity();

    public IEnumerable<CepEntity> Ceps { get; set; }
}
