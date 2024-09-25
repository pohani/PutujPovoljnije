namespace PutujPovoljnije.Domain.Models
{
    public class FlightSearch
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SearchString { get; set; }
        public string DepartureAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string DepartureDate { get; set; }
        public string? ReturnDate { get; set; }
        public int Adults { get; set; }
        public int? Children { get; set; }
        public int? Infants { get; set; }
        public string? TravelClass { get; set; }
        public string? IncludedAirlineCodes { get; set; }
        public string? ExcludedAirlineCodes { get; set; }
        public bool? NonStop { get; set; }
        public string Currency { get; set; }
        public int? MaxPrice { get; set; }
        public int? MaxOffers { get; set; }

        // Navigation property for the one-to-many relationship
        public List<FlightOffer> FlightOffers { get; set; } = new List<FlightOffer>();
    }
}
