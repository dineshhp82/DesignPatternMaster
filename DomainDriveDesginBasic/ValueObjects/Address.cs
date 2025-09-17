namespace DomainDriveDesignBasic.ValueObjects
{
    // Simple value object with immutable and no factory method but validate before initalized the object
    public record Address
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string Country { get; init; }

        public Address(string street, string city, string country)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street required");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City required");
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country required");

            Street = street;
            City = city;
            Country = country;
        }
    }
}
