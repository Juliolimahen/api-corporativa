using CT.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CT.Data.Configuration;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(e => e.ClienteId);
        builder.Property(e => e.Estado).HasConversion(
            e => e.ToString(),
            e => (Estado)Enum.Parse(typeof(Estado), e));
    }
}
