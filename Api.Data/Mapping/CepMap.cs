
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping;

public class CepMap : IEntityTypeConfiguration<CepEntity>
{
    public void Configure(EntityTypeBuilder<CepEntity> builder)
    {
        builder.ToTable("Cep");

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Cep);            

        builder.HasOne(u => u.Municipio)
            .WithMany(m => m.Ceps);
    }
}



