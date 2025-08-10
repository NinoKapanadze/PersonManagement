using PersonManagement.Shared;

namespace PersonManagement.Application.DTOs
{
    public record PhoneNumberDto(
    string Number,
    PhoneType PhoneType
    );
}
