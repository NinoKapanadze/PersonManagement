using Microsoft.EntityFrameworkCore;
using PersonManagement.Domain;
using PersonManagement.Domain.Entities;
using PersonManagement.Infrastructure.Configurations;

namespace PersonManagement.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }
        public DbSet<Experience> Experiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
