using CT.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.Data.Configuration
{
    public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Telefone> builder)
        {
            //builder.HasOne(p => p.Cliente).WithMany(p => p.Telefones).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasKey(p => new { p.ClienteId, p.Numero });//Chave composta
        }
    }
}
