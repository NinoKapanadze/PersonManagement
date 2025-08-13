using FluentValidation;
using PersonManagement.Domain.Validators;

namespace PersonManagement.Domain
{
    public class Person : BaseEntity<int>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool? Gender { get; private set; }
        public string PersonalIdNumber { get; private set; }
        public DateOnly BirthDay { get; private set; }
        public List<PhoneNumber> PhoneNumbers { get; private set; }  = new();
        public List<RelatedPerson> RelatedPersons { get; private set; } = new();

        protected Person() { }
        private Person(
            string firstName,
            string lastName,
            bool? gender,
            string personalIdNumber,
            DateOnly birthDay,
            List<PhoneNumber> phoneNumbers,
            List<RelatedPerson> relatedPersons)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalIdNumber = personalIdNumber;
            BirthDay = birthDay;
            PhoneNumbers = phoneNumbers ?? new List<PhoneNumber>();
            RelatedPersons = relatedPersons ?? new List<RelatedPerson>();

            CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
        }

        public static Person Create(
                string firstName,
                string lastName,
                bool? gender,
                string personalIdNumber,
                DateOnly birthDay,
                List<PhoneNumber>? phoneNumbers = null,
                List<RelatedPerson>? relatedPersons = null)
        {
            var person = new Person(
                firstName,
                lastName,
                gender,
                personalIdNumber,
                birthDay,
                phoneNumbers ?? new List<PhoneNumber>(),
                relatedPersons ?? new List<RelatedPerson>()
            );

            var validator = new PersonValidator();
            var validationResult = validator.Validate(person);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return person;
        }


        public void Update(
                string firstName,
                string lastName,
                bool? gender,
                string personalIdNumber,
                DateOnly birthDay,
                List<PhoneNumber>? phoneNumbers = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalIdNumber = personalIdNumber;
            BirthDay = birthDay;

            if (phoneNumbers != null)
            {
                PhoneNumbers = phoneNumbers;
            }

            var validator = new PersonValidator();
            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
