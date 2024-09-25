
namespace PutujPovoljnije.Application.DTOs
{
    public class FlightOfferDto
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? Source { get; set; }
        public bool? InstantTicketingRequired { get; set; }
        public bool? NonHomogeneous { get; set; }
        public bool? OneWay { get; set; }
        public bool? IsUpsellOffer { get; set; }
        public DateTime? LastTicketingDate { get; set; }
        public DateTime? LastTicketingDateTime { get; set; }
        public int? NumberOfBookableSeats { get; set; }
        public List<ItineraryDto> Itineraries { get; set; } = new List<ItineraryDto>();
        public PriceDto Price { get; set; }
        public List<string> ValidatingAirlineCodes { get; set; } = new List<string>();
        public Guid FlightSearchId { get; set; }
    }

    public class ItineraryDto
    {
        public Guid Id { get; set; }
        public string? Duration { get; set; }
        public List<SegmentDto> Segments { get; set; } = new List<SegmentDto>();
    }

    public class SegmentDto
    {
        public Guid Id { get; set; }
        public DepartureDto Arrival { get; set; }
        public DepartureDto Departure { get; set; }
        public string? CarrierCode { get; set; }
        public string? Number { get; set; }
        public string? Duration { get; set; }
        public int? NumberOfStops { get; set; }
        public bool? BlacklistedInEU { get; set; }
    }

    public class DepartureDto
    {
        public Guid Id { get; set; }
        public string? IataCode { get; set; }
        public string? Terminal { get; set; }
        public DateTime? At { get; set; }
    }

    public class PriceDto
    {
        public Guid Id { get; set; }
        public string? Currency { get; set; }
        public string? Total { get; set; }
        public string? Base { get; set; }
        public string? GrandTotal { get; set; }
    }
}