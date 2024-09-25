namespace PutujPovoljnije.Domain.Models
{
    public class Airport
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string IATA { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

    }
}
