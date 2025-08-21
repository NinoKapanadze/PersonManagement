using PersonManagement.Shared;

namespace PersonManagement.Domain
{
    public class PhoneNumber : BaseEntity<int>
    {
        public string Number { get; private set; }
        public PhoneType PhoneType { get; private set; }
        public int PersonId { get; private set; }
        public Person Person { get; private set; }

        private PhoneNumber(string number, PhoneType phoneType)
        {
            Number = number;
            PhoneType = phoneType;
        }
        private PhoneNumber() { }
        public static PhoneNumber Create(string number, PhoneType phoneType) 
        {
            return new PhoneNumber(number, phoneType);
        }
    }
}
