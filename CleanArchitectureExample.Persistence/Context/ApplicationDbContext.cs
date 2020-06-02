using CleanArchitectureExample.Domain;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Persistence.EntityConfig;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CleanArchitectureExample.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public Person Person { get; set; }
        public PersonPhone PersonPhone { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
        public BookLoan BookLoan {get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfiguration(new PersonPhoneConfig());
            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookLoanConfig());
        }
    }
}
