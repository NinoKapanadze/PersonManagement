namespace PersonManagement.Application.DTOs
{
    public record PersonDTO(
      int Id,
      string FirstName,
      string LastName,
      bool? Gender,
      string PersonalIdNumber,
      DateOnly BirthDay,
      List<PhoneNumberDto> PhoneNumbers,
      List<RelatedPersonDTO> RelatedPersons,
      List<ExperienceDTO> Experiences);
    
}
