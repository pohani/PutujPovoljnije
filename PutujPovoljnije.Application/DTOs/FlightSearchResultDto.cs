namespace PutujPovoljnije.Application.DTOs
{
    public class FlightSearchResultDto
    {
        public List<FlightOfferDto> FlightOffers { get; set; }
        public string DepartureAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string DepartureDate { get; set; }
        public string? ReturnDate { get; set; }
        public int Adults { get; set; }
        public int? Children { get; set; }
        public string Currency { get; set; }

    }
}
