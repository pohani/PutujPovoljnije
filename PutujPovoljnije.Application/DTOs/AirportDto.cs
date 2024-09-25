﻿namespace PutujPovoljnije.Application.DTOs
{
    public class AirportDto
    {
        public Guid Id { get; set; }
        public string IATA { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; }
    }
}