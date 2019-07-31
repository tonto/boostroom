using System.Threading.Tasks;
using BoostRoom.Accounts.Application.Commands;
using BoostRoom.WebApp.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoostRoom.WebApp.Controllers.Accounts
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/accounts/sellers")]
    public class SellerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterSeller command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}