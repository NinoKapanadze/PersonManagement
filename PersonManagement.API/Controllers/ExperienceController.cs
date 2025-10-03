using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagement.Application.Experiences.Commands.AddExperienceToPerson;

namespace PersonManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ILogger<ExperienceController> _logger; //TODO: add logging
        private readonly IMediator _mediator;

        public ExperienceController(ILogger<ExperienceController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        /// <summary>
        /// Adds an experience for a person.
        /// </summary>
        /// <param name="command">The command containing experience details to add to the person.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> AddExperienceForPerson(AddExperienceToPersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
