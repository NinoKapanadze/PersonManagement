using FluentValidation;
using PersonManagement.Domain.Entities;
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
        public string ProfessionalSummary { get; private set; } = string.Empty;

        public List<PhoneNumber> PhoneNumbers { get; private set; }  = new();
        public List<RelatedPerson> RelatedPersons { get; private set; } = new();
        public List<Experience> Experiences { get; private set; } = new();

        private Person() { }
        private Person(
            string firstName,
            string lastName,
            bool? gender,
            string personalIdNumber,
            DateOnly birthDay,
            List<PhoneNumber>? phoneNumbers,
            List<RelatedPerson>? relatedPersons,
            List<Experience>? experiences)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalIdNumber = personalIdNumber;
            BirthDay = birthDay;
            PhoneNumbers = phoneNumbers ?? new List<PhoneNumber>();
            RelatedPersons = relatedPersons ?? new List<RelatedPerson>();
            Experiences = experiences ?? new List<Experience>();

        }

        public static Person Create(
                string firstName,
                string lastName,
                bool? gender,
                string personalIdNumber,
                DateOnly birthDay,
                List<PhoneNumber>? phoneNumbers = null,
                List<RelatedPerson>? relatedPersons = null,
                List<Experience>? experiences = null)
        {
            var person = new Person(
                firstName,
                lastName,
                gender,
                personalIdNumber,
                birthDay,
                phoneNumbers ?? new List<PhoneNumber>(),
                relatedPersons ?? new List<RelatedPerson>()
                experiences ?? new List<Experience>()

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

            var validator = new PersonValidator();
            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void UpdatePhoneNumbers(IEnumerable<PhoneNumber> newNumbers)
        {

            foreach (var phone in PhoneNumbers)
            {
                if (newNumbers.Any(newPhone => newPhone.Number == phone.Number && newPhone.PhoneType == phone.PhoneType) == false)
                {
                    phone.MarkAsDeleted();
                }
            }

            foreach (var item in newNumbers)
            { 
                var existingPhone = PhoneNumbers.FirstOrDefault(p => p.Number == item.Number && p.PhoneType == item.PhoneType);

                if (existingPhone == null)
                {
                    PhoneNumbers.Add(item);
                }
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
