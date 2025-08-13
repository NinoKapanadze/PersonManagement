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

        public RelatedPerson(int personId, int relatedToId, RelationshipType relationshipType)
        {
            PersonId = personId;
            RelatedToId = relatedToId;
            RelationshipType = relationshipType;
            CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
        }


    }
    
}
