using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagement.Application.Persons.Commands.CreatePerson;

namespace PersonManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateLoanApplication")]
        public async Task<IActionResult> CreatePerson(CreatePersonCommand createPersonCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(createPersonCommand, cancellationToken);
            return Ok();    
        }
       

    }
}
