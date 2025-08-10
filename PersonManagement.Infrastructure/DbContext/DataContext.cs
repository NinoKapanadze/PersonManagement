using Microsoft.EntityFrameworkCore;
using PersonManagement.Domain;
using PersonManagement.Infrastructure.Configurations;

namespace PersonManagement.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());
            modelBuilder.ApplyConfiguration(new RelatedPersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
