namespace SuperMarket.ValueObjects
{
    //Immutable Value object because this have no identity
    public record class Money(decimal Amount, string Currency)
    {
        public static Money Zero => new(0, "USD");

        // Operator overloading for adding two Money objects
        public Money Add(Money other) => Currency == other.Currency
                 ? new Money(Amount + other.Amount, Currency)
                 : throw new InvalidOperationException("Currency mismatch");

        // Operator overloading for multiply money with some factor
        public Money Multiply(decimal factor) => new(Amount * factor, Currency);

        public Money Negate() => new(-Amount, Currency);
    }
}
