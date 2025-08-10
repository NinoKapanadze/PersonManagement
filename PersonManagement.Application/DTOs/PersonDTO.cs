namespace PersonManagement.Application.DTOs
{
    public record PersonDTO(
      string FirstName,
      string LastName,
      bool? Gender,
      string PersonalIdNumber,
      DateOnly BirthDay,
      List<PhoneNumberDto> PhoneNumbers)
    {
    }
}
