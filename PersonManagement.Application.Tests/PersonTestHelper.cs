using PersonManagement.Domain;
using PersonManagement.Shared;

namespace PersonManagement.Application.Tests
{
    public static class PersonTestHelper
    {
        public static Person CreatePerson(
            int id = 1,
            string firstName = "John",
            string lastName = "Doe",
            bool gender = true,
            string personalId = "12345678901",
            DateOnly? birthDate = null,
            List<PhoneNumber>? phoneNumbers = null,
            List<RelatedPerson>? relatedPersons = null)
        {
            var person = Person.Create(
                firstName,
                lastName,
                gender,
                personalId,
                birthDate ?? new DateOnly(1990, 1, 1),
                phoneNumbers ?? new List<PhoneNumber>
                {
                    PhoneNumber.Create("5551234", PhoneType.Mobile)
                },
                relatedPersons ?? new List<RelatedPerson>()
            );

            typeof(Person).GetProperty("Id")!.SetValue(person, id);

            return person;
        }

        public static (Person person, Person relatedPerson) CreatePersonWithRelation()
        {
            var person = CreatePerson(1, "John", "Doe", true);
            var relatedPerson = CreatePerson(2, "Jane", "Doe", false);

            var relation = RelatedPerson.Create(person, relatedPerson, RelationshipType.Relative);
            person.RelatedPersons.Add(relation);

            return (person, relatedPerson);
        }
    }
}

