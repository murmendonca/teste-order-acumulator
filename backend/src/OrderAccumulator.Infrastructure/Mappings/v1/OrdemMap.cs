using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAccumulator.Domain.Entities.v1;

namespace OrderAccumulator.Infrastructure.Mappings.v1;

public class OrdemMap : IEntityTypeConfiguration<Ordem>
{
    public void Configure(EntityTypeBuilder<Ordem> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Ativo).HasConversion<string>();
        builder.Property(o => o.Lado).HasConversion<string>();
    }
}