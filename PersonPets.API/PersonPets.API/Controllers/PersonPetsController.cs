using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonPets.API.Filters;
using PersonPets.API.Services.Interfaces;

namespace PersonPets.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonPetsController : ControllerBase
    {
        private readonly ILogger<PersonPetsController> _logger;
        private readonly IPeopleService _peopleService;

        public PersonPetsController(ILogger<PersonPetsController> logger, IPeopleService peopleService)
        {
            _logger = logger;
            _peopleService = peopleService;
        }

        [HttpGet]
        [ApiKeyAuth]
        public async Task<IActionResult> GetPetsData(
            [FromHeader(Name = Constants.ApiKeyHeaderName)]
            [Required] string apiKey,
            string petType)
        {
            try
            {
                var results = await _peopleService.GetPetNamesGroupedUponOwnerGender(petType);
                return Ok(results);
            }
            catch(FluentValidation.ValidationException ex)
            {
                _logger.LogError(ex, "Internal validaion failed while getting Pet Names data based upon owners gender");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed while getting Pet Names data based upon owners gender");
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}
