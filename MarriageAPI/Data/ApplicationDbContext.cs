using MarriageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarriageAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Marriage> Marriages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(p => new { p.PersonalCode}) // PersonalCode as index
                .IsUnique(true); // prevents duplicates
        }
    }
}
