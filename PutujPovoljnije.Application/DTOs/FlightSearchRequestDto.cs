using System.ComponentModel.DataAnnotations;

namespace PutujPovoljnije.Application.DTOs
{
    public class FlightSearchRequestDto
    {
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Departure airport code must be a 3-letter code.")]
        public string DepartureAirport { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Destination airport code must be a 3-letter code.")]
        public string DestinationAirport { get; set; }

        [Required(ErrorMessage = "Departure date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Departure date format is YYYY-MM-DD.")]
        public string DepartureDate { get; set; }

        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Return date format is YYYY-MM-DD.")]
        public string ReturnDate { get; set; }

        [Required(ErrorMessage = "At least one adult is needed.")]
        [Range(1, 9, ErrorMessage = "Adults must be between 1 and 9.")]
        public int Adults { get; set; }

        [Range(0, 9, ErrorMessage = "Children must be between 0 and 9.")]
        public int Children { get; set; }

        [Range(0, 18, ErrorMessage = "Total number of travelers can't exceed 18.")]
        public int TotalTravelers => Adults + Children;

        [Required(ErrorMessage = "Currency code must be a 3-letter code.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency code must be a 3-letter code.")]
        public string Currency { get; set; }
    }
}
