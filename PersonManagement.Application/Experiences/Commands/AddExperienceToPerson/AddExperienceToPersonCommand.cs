using MediatR;

namespace PersonManagement.Application.Experiences.Commands.AddExperienceToPerson
{
    public record AddExperienceToPersonCommand(
        int PersonId,
        List<string> skills,
        DateTime StartDate,
        DateTime? EndDate ,
        string Position,
        string CompanyName
         ) : IRequest<bool>
    {
    }
}
