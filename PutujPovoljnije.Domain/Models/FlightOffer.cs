using Newtonsoft.Json;

namespace PutujPovoljnije.Domain.Models
{
    public class FlightOffer
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Type { get; set; }
        public string? Source { get; set; }
        public bool? InstantTicketingRequired { get; set; }
        public bool? NonHomogeneous { get; set; }
        public bool? OneWay { get; set; }
        public bool? IsUpsellOffer { get; set; }
        public DateTime? LastTicketingDate { get; set; }
        public DateTime? LastTicketingDateTime { get; set; }
        public int? NumberOfBookableSeats { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public Price Price { get; set; }
        public List<string> ValidatingAirlineCodes { get; set; }

        // Foreign key to FlightSearch
        public Guid FlightSearchId { get; set; }
        // Navigation property back to FlightSearch
        public FlightSearch FlightSearch { get; set; }

    }

    public class Itinerary
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Duration { get; set; }
        public List<Segment> Segments { get; set; } = new List<Segment>();
    }

    public class Segment
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Departure Arrival { get; set; }
        public Departure Departure { get; set; }
        public string? CarrierCode { get; set; }
        public string? Number { get; set; }
        public string? Duration { get; set; }
        public int? NumberOfStops { get; set; }
        public bool? BlacklistedInEU { get; set; }
    }

    public class Departure
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? IataCode { get; set; }
        public string? Terminal { get; set; }
        public DateTime? At { get; set; }
    }

    public class Price
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Currency { get; set; }
        public string? Total { get; set; }
        public string? Base { get; set; }
        public string? GrandTotal { get; set; }
    }

    public class Meta
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? Count { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Self { get; set; }
    }

    public class ApiResponse
    {
        [JsonConverter(typeof(JsonConvert.GuidConverter))]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Meta Meta { get; set; }
        public List<FlightOffer> Data { get; set; } = new List<FlightOffer>();
    }

}
