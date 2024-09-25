using Microsoft.AspNetCore.Mvc;
using PutujPovoljnije.Application.DTOs;
using PutujPovoljnije.Application.Interfaces;

namespace PutujPovoljnije.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportsService _airportsService;
        private readonly ILogger<AirportsController> _logger;

        public AirportsController(IAirportsService airportsService, ILogger<AirportsController> logger)
        {
            _airportsService = airportsService;
            _logger = logger;
        }

        [HttpGet("GetPageable")]
        public async Task<IActionResult> GetAirports(int page = 0, int limit = 20)
        {
            var offset = page * limit;

            _logger.LogInformation("Fetching airports with offset: {Offset} and limit: {Limit}", offset, limit);

            var airports = await _airportsService.GetAirports(offset, limit);

            if (airports == null || airports.Count == 0)
            {
                _logger.LogWarning("No airports found with offset: {Offset} and limit: {Limit}", offset, limit);
                return NotFound(new { success = false, message = "No airports found." });
            }

            _logger.LogInformation("Successfully fetched {Count} airports.", airports.Count);
            return Ok(airports);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AirportDto>>> GetAllAirports()
        {
            _logger.LogInformation("Fetching all airports.");

            var airports = await _airportsService.GetAllAirports();

            if (airports == null || airports.Count == 0)
            {
                _logger.LogWarning("No airports found.");
                return NotFound(new { success = false, message = "No airports found." });
            }

            _logger.LogInformation("Successfully fetched {Count} airports.", airports.Count);
            return Ok(new { success = true, data = airports });
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AirportDto>>> Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                _logger.LogWarning("Search string is empty or null.");
                return BadRequest(new { success = false, message = "Search string cannot be empty." });
            }

            _logger.LogInformation("Searching for airports with search string: {SearchString}", searchString);

            var airports = await _airportsService.GetAirportsFromString(searchString);

            if (airports == null || airports.Count == 0)
            {
                _logger.LogWarning("No airports found for search string: '{SearchString}'", searchString);
                return NotFound(new { success = false, message = $"No airports found for search string '{searchString}'." });
            }

            _logger.LogInformation("Successfully found {Count} airports for search string: '{SearchString}'", airports.Count, searchString);
            return Ok(new { success = true, data = airports });
        }
    }
}
