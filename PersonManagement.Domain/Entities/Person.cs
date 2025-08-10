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
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(personalIdNumber))
                throw new ArgumentException("Personal ID number is required.", nameof(personalIdNumber));

            if (birthDay == default)
                throw new ArgumentException("Birth date is required.", nameof(birthDay));

            return new Person(
                firstName,
                lastName,
                gender,
                personalIdNumber,
                birthDay,
                phoneNumbers ?? new List<PhoneNumber>(),
                relatedPersons ?? new List<RelatedPerson>()
            );
        }
    }
}
