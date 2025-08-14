using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagement.Application.Persons.Commands.AddRelatedPerson;
using PersonManagement.Application.Persons.Commands.CreatePerson;
using PersonManagement.Application.Persons.Commands.DeletePerson;
using PersonManagement.Application.Persons.Commands.UpdatePerson;
using PersonManagement.Application.Persons.Queries.GetPersonsList;
using PersonManagement.Application.Persons.Queries.GetPersonWithId;

namespace PersonManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing person-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="createPersonCommand">The person data to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created person ID.</returns>
        [HttpPost("CreatePerson")]
        public async Task<IActionResult> CreatePerson(CreatePersonCommand createPersonCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createPersonCommand, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="updatePersonCommand">The updated person data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated person.</returns>
        [HttpPut("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson(UpdatePersonCommand updatePersonCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updatePersonCommand, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Adds person and add relationship to an existing person.
        /// </summary>
        /// <param name="createPersonCommand">The person data to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created person ID.</returns>
        [HttpPost("AddRelatedPerson")]
        public async Task<IActionResult> AddRelatedPerson(AddRelatedPersonCommand createPersonCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createPersonCommand, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns the result of the delete operation.</returns>
        [HttpDelete("DeletePerson/{id}")]
        public async Task<IActionResult> DeletePerson(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePersonCommand { Id = id }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a person with details by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The person with the specified ID.</returns>
        [HttpGet("GetPersonWithId/{id}")]
        public async Task<IActionResult> GetPersonWithId(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPersonWithIdQuery (id), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a paginated list of all persons, optionally filtered and sorted.
        /// </summary>
        /// <param name="getPersonList">Query parameters for filtering, sorting, and pagination.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paginated list of persons.</returns>
        [HttpGet("GetAllPersons")]
        public async Task<IActionResult> GetAllPersons([FromQuery] GetAllPersonsListQuery getPersonList, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getPersonList, cancellationToken);
            return Ok(result);
        }
    }
}
