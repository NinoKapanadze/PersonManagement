namespace PersonManagement.Application.DTOs
{
    public record ExperienceDTO(
        string Position,
        List<string> Skills,
        DateTime StartDate,
        DateTime? EndDate,
        string CompanyName
    );  
}
