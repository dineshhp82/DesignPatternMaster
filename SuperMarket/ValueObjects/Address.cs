namespace SuperMarket.ValueObjects
{
    //Immutable and Value Object because this have no identity
    public record Address(
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country)
    {
        public bool IsValid()
        {
            // Basic validation logic
            return !string.IsNullOrWhiteSpace(Street) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(Country);
        }
    }
}
