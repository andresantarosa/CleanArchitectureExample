using CleanArchitectureExample.Domain;
using CleanArchitectureExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Persistence.EntityConfig
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Title)
                   .HasColumnType("varchar(255)")
                   .IsRequired();

            builder.Property(b => b.Edition)
                   .IsRequired();

            builder.Property(b => b.ReleaseYear)
                   .IsRequired();

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();

            builder.Property(b => b.ISBN)
                .HasColumnType("varchar(13)")
                .IsRequired();

            builder.OwnsOne(pt => pt.BookSituation, bookSituation =>
            {
                bookSituation.Property(tt => tt.Value)
                .IsRequired()
                .HasColumnName("BookSituation")
                .HasColumnType("integer");

                bookSituation.Ignore(tt => tt.Name);
            });
        }
    }
}
