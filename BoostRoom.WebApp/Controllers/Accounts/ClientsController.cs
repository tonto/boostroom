using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoostRoom.Accounts.Application.Commands;
using BoostRoom.WebApp.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoostRoom.WebApp.Controllers.Accounts
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/accounts/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterClientDto request)
        {
            await _mediator.Send(
                new RegisterClient(
                    request.Username,
                    request.Email,
                    request.Password,
                    request.FirstName,
                    request.LastName,
                    request.AddressLine,
                    request.City,
                    request.Zip,
                    request.Country,
                    request.SubscribeToOffers
                )
            );

            return NoContent();    
        }
    }
}