using CleanArchitectureExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Persistence.EntityConfig
{
    public class BookLoanConfig : IEntityTypeConfiguration<BookLoan>
    {
        public void Configure(EntityTypeBuilder<BookLoan> builder)
        {
            builder.HasKey(bl => bl.BookLoanId);

            builder.HasOne(bl => bl.Taker)
                .WithMany(p => p.BookLoans)
                .HasForeignKey(bl => bl.TakerId)
                .IsRequired();

            builder.HasOne(bl => bl.Book)
                .WithMany(b => b.BookLoans)
                .HasForeignKey(bl => bl.BookId)
                .IsRequired();
        }
    }
}
