namespace PersonManagement.Domain.Entities
{
    public class Experience :BaseEntity<int>
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public string Position { get; set; }  = string.Empty;
        public string Skills { get; set; }  = string.Empty;
        public int Years { get; set; }   
    }
}
