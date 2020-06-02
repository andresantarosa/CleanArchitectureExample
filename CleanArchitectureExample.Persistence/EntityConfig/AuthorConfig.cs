using CleanArchitectureExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Persistence.EntityConfig
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.AuthorId);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
