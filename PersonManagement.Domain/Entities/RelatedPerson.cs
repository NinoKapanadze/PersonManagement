using PersonManagement.Shared;

namespace PersonManagement.Domain
{
    public class RelatedPerson : BaseEntity<int>
    {
        public int PersonId { get; private set; }     
        public Person Person { get; private set; }

        public int RelatedToId { get; private set; }  
        public Person RelatedTo { get; private set; }

        public RelationshipType RelationshipType { get; private set; }

        private RelatedPerson() { } 

        private RelatedPerson(int personId, int relatedToId, RelationshipType relationshipType)
        {
            PersonId = personId;
            RelatedToId = relatedToId;
            RelationshipType = relationshipType;
        }
        private RelatedPerson(Person person, Person relatedTo, RelationshipType relationshipType)
        {
            Person = person;
            RelatedTo = relatedTo;
            RelationshipType = relationshipType;
            PersonId = person.Id;
            RelatedToId = relatedTo.Id;
        }
        public static RelatedPerson Create(int personId, int relatedToId, RelationshipType relationshipType)
        {
            return new RelatedPerson(personId, relatedToId, relationshipType);
        }
        public static RelatedPerson Create(Person person, Person relatedTo, RelationshipType relationshipType)
        {
            return new RelatedPerson(person, relatedTo, relationshipType);
        }

    }
    
}
