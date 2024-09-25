using Microsoft.AspNetCore.Mvc;
using PutujPovoljnije.Application.Interfaces;

namespace PutujPovoljnije.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataRefreshController : ControllerBase
    {
        private readonly IRefreshDataService _refreshDataService;
        private readonly ILogger<DataRefreshController> _logger;

        public DataRefreshController(IRefreshDataService refreshDataService, ILogger<DataRefreshController> logger)
        {
            _refreshDataService = refreshDataService;
            _logger = logger;
        }

        [HttpPost("airports")]
        public async Task<ActionResult> RefreshAirports()
        {
            _logger.LogInformation("Starting the refresh process for airports data.");

            try
            {
                await _refreshDataService.RefreshAirports();
                _logger.LogInformation("Airports data refreshed successfully.");

                return Ok(new { success = true, message = "Airports data refreshed successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while refreshing airports data.");
                return StatusCode(500, new { success = false, message = "An unexpected error occurred during the refresh process." });
            }
        }
    }
}
