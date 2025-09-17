namespace DomainDriveDesignBasic.ValueObjects
{
    public record class OrderId(Guid Value)
    {
        public static OrderId New() => new OrderId(Guid.NewGuid());
    }
}
