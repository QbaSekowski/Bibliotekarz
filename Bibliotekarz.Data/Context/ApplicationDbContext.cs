using Bibliotekarz.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekarz.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions option) : base(option)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connstr = "Server=localhost;Initial Catalog=BibliotekarzDb;User Id=sa;Password=***";
            string connstr = "Server=localhost;Initial Catalog=BibliotekarzDb;Integrated Security=true;TrustServerCertificate=True;MultipleActiveResultSets=True;";
            optionsBuilder.UseSqlServer(connstr)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Customer> Borrowers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>().Property(e => e.Author).IsRequired();
            builder.Entity<Book>().Property(e => e.Title).HasMaxLength(1000);

            builder.Entity<Book>()
                .HasOne(e => e.Borrower)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
// W konsoli menedżera NuGet:
// add-Migration Initial
// Update-Database