using PersonManagement.Shared;

namespace PersonManagement.Application.DTOs
{
    public record RelatedPersonDTO(
    PersonDTO RelatedPerson,
    RelationshipType RelationshipType);
}
