
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping;

public class UfMap : IEntityTypeConfiguration<UfEntity>
{
    public void Configure(EntityTypeBuilder<UfEntity> builder)
    {
        builder.ToTable("Uf");

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Sigla)
            .IsUnique();
    }
}
