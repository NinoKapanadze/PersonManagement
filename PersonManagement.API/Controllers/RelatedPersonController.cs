using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagement.Application.Persons.Commands.AddRelatedPerson;

namespace PersonManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedPersonController : ControllerBase
    { 
        IMediator _mediator;
        public RelatedPersonController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Adds person and add relationship to an existing person.
        /// </summary>
        /// <param name="createPersonCommand">The person data to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created person ID.</returns>
        [HttpPost]
        public async Task<IActionResult> AddRelatedPerson(AddRelatedPersonCommand createPersonCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createPersonCommand, cancellationToken);
            return Ok(result);
        }
    }
}
