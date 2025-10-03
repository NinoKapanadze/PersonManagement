namespace PersonManagement.Domain.Entities
{
    public class Experience :BaseEntity<int>
    {
        public int PersonId { get; private set; } 
        public Person Person { get;  set; }

        public string Position { get; private set; }  = string.Empty;
        public List<string> Skills { get; private set; } = new List<string>();
        public DateTime StartDate { get; private set; }   
        public DateTime? EndDate { get; private set; } 
        public string CompanyName { get; private set; } = string.Empty;
        private Experience()
        {
            
        }
        public Experience(string position, 
            List<string> skills, 
            DateTime startDate, 
            DateTime? endDate,
            string CompanyName,
            Person? person
        )
        {
            Position = position;
            Skills = skills;
            StartDate = startDate;
            EndDate = endDate;
            this.CompanyName = CompanyName;
            Person = person ?? throw new ArgumentNullException(nameof(person));
        }
    }
}
