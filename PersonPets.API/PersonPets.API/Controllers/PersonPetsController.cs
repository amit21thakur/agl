using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PersonPets.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonPetsController : ControllerBase
    {
        private readonly ILogger<PersonPetsController> _logger;

        public PersonPetsController(ILogger<PersonPetsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPetsData()
        {
            return Ok();
        }
    }
}
