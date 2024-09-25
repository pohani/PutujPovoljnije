using Microsoft.AspNetCore.Mvc;
using PutujPovoljnije.Application.DTOs;
using PutujPovoljnije.Application.Interfaces;

namespace PutujPovoljnije.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightSearchController : ControllerBase
    {
        private readonly IFlightSearchService _flightSearchService;
        private readonly ILogger<FlightSearchController> _logger;

        private const string NoFlightsMessage = "No flights found for the given criteria.";

        public FlightSearchController(IFlightSearchService flightSearchService, ILogger<FlightSearchController> logger)
        {
            _flightSearchService = flightSearchService;
            _logger = logger;
        }

        [HttpPost("search")]
        public async Task<ActionResult<object>> Search([FromBody] FlightSearchRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for flight search request.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Searching for flights with the following criteria: {@Request}", request);

            try
            {
                var flightSearchResult = await _flightSearchService.SearchFlightsAsync(request);

                if (flightSearchResult == null)
                {
                    _logger.LogInformation(NoFlightsMessage);
                    return NotFound(new { success = false, message = NoFlightsMessage });
                }

                _logger.LogInformation("Flights found successfully.");
                return Ok(new { success = true, data = flightSearchResult });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the flight search request.");
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
    }
}
