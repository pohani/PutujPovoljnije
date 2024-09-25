using AutoMapper;
using PutujPovoljnije.Application.DTOs;
using PutujPovoljnije.Domain.Models;

namespace PutujPovoljnije.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FlightSearchRequestDto, FlightSearch>();

            CreateMap<ItineraryDto, Itinerary>();
            CreateMap<Itinerary, ItineraryDto>();
            CreateMap<SegmentDto, Segment>();
            CreateMap<Segment, SegmentDto>();
            CreateMap<DepartureDto, Departure>();
            CreateMap<Departure, DepartureDto>();
            CreateMap<PriceDto, Price>();
            CreateMap<Price, PriceDto>();


            CreateMap<FlightOfferDto, FlightOffer>();
            CreateMap<FlightOffer, FlightOfferDto>();

            CreateMap<FlightSearch, FlightSearchResultDto>();

            CreateMap<Airport, AirportDto>();




        }
    }
}
