using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoostRoom.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoostRoom.WebApp.Controllers.Accounts
{
    [ApiController]
    [Route("api/accounts/clients")]
    public class ClientsController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterClientDto request)
        {
            return Ok("yo yo");
        }
    }
}