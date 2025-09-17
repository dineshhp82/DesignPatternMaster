namespace SuperMarket.ValueObjects
{
    public record OrderId(Guid Value)
    {
        public static OrderId New() => new(Guid.NewGuid());
    }
}
