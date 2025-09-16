using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Domain;
using PersonManagement.Shared;

namespace PersonManagement.Infrastructure.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork, DataContext dbcontext, CancellationToken cancellationToken = default)
        {
            if (dbcontext.Persons.Any())
                return;

            // Example persons with related data
            var person1 = Person.Create
            (
                 "John",
                 "Doe",
                 true,
                 "12345678901",
                 new DateOnly(1994, 5, 15),
                 new List<PhoneNumber>
                 {
                    PhoneNumber.Create("0322123123", PhoneType.House),
                    PhoneNumber.Create("579370767", PhoneType.Mobile)
                 }
            );

            var person2 = Person.Create
            (
                 "Jane",
                "Smith",
                 false,
                "10987654321",
                new DateOnly(1994, 5, 15),
                new List<PhoneNumber>
                {
                    PhoneNumber.Create("1231237", PhoneType.Office)
                }
            );

            var relation = RelatedPerson.Create(person1, person2, RelationshipType.Acquaintance);
            person1.RelatedPersons.Add(relation);

            unitOfWork.PersonWriteRepository.Add(person1);
            unitOfWork.PersonWriteRepository.Add(person2);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}


