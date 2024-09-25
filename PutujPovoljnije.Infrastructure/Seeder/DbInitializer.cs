using Microsoft.EntityFrameworkCore;
using PutujPovoljnije.Domain.Models;
using PutujPovoljnije.Infrastructure.Data;

namespace PutujPovoljnije.Infrastructure.Seeder
{
    public class DbInitializer
    {
        private readonly FlightSearchDbContext _context;

        public DbInitializer(FlightSearchDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.FlightSearches.AnyAsync())
            {

                FlightSearch flightSearch = new FlightSearch
                {
                    SearchString = "NYC to LAX",
                    DepartureAirport = "JFK",
                    DestinationAirport = "LAX",
                    DepartureDate = "2023-12-01",
                    ReturnDate = "2023-12-15",
                    Adults = 1,
                    Children = 0,
                    Infants = 0,
                    TravelClass = "Economy",
                    IncludedAirlineCodes = "AA, DL",
                    ExcludedAirlineCodes = "UA",
                    NonStop = true,
                    Currency = "USD",
                    MaxPrice = 500,
                    MaxOffers = 5
                };

                await _context.FlightSearches.AddAsync(flightSearch);
                await _context.SaveChangesAsync();

                Price price = new Price
                {
                    Currency = "USD",
                    Total = "300",
                    Base = "250",
                    GrandTotal = "300"
                };

                await _context.Prices.AddAsync(price);
                await _context.SaveChangesAsync();

                FlightOffer flightOffer = new FlightOffer
                {
                    FlightSearchId = flightSearch.Id,
                    Type = "Standard",
                    Source = "Amadeus",
                    InstantTicketingRequired = true,
                    NonHomogeneous = false,
                    OneWay = false,
                    IsUpsellOffer = false,
                    LastTicketingDate = DateTime.Now.AddDays(10),
                    LastTicketingDateTime = DateTime.Now.AddHours(5),
                    NumberOfBookableSeats = 5,
                    Price = price,
                    ValidatingAirlineCodes = new List<string> { "AA" }
                };

                await _context.FlightOffers.AddAsync(flightOffer);
                await _context.SaveChangesAsync();

                Itinerary itinerary = new Itinerary
                {
                    Duration = "6h 30m",
                    Segments = new List<Segment>
                {
                    new Segment
                    {
                        Departure = new Departure
                        {
                            IataCode = "JFK",
                            Terminal = "4",
                            At = DateTime.Now.AddDays(10)
                        },
                        Arrival = new Departure
                        {
                            IataCode = "LAX",
                            Terminal = "B",
                            At = DateTime.Now.AddDays(10).AddHours(6)
                        },
                        CarrierCode = "AA",
                        Number = "100",
                        Duration = "6h 30m",
                        NumberOfStops = 0,
                        BlacklistedInEU = false
                    }
                }
                };

                await _context.Itineraries.AddAsync(itinerary);
                await _context.SaveChangesAsync();

                flightOffer.Itineraries = new List<Itinerary> { itinerary };
            }



            if (!await _context.Airports.AnyAsync())
            {

                Airport airport = new Airport { City = "Zagreb", IATA = "ZGR", Country = "Croatia", Name = "Franjo Tuđman" };

                await _context.Airports.AddAsync(airport);
            }


            await _context.SaveChangesAsync();
        }
    }
}
