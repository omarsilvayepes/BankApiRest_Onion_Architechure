using Application.Features.Commands.CreateClientCommand;
using Application.Features.Commands.DeleteClientCommand;
using Application.Features.Commands.UpdateClientCommand;
using Application.Features.Queries.GetAllClients;
using Application.Features.Queries.GetClientById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles ="Admin")]// only authenticate user with Admin Role
        public async Task<IActionResult> Post(CreateClientCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, UpdateClientCommand updateClientCommand)
        {
            if (id != updateClientCommand.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(updateClientCommand));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClientCommand { Id = id }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles="Basic")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClientByIdQuery { Id = id }));
        }

        [HttpGet()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllClientParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClientsQuery 
            {   PageNumber=filter.PageNumber,
                PageSize=filter.PageSize,
                FirstName=filter.FirstName,
                LastName=filter.LastName
            }));
        }

    }
}
