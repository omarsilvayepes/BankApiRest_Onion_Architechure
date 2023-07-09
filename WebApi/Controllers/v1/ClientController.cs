using Application.Features.Commands.CreateClientCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreateClientCommand createClientCommand)
        {
            return Ok(await Mediator.Send(createClientCommand));
        }
    }
}
