namespace DomainDriveDesignBasic.ValueObjects
{
    public record Money
    {
        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; init; }
        public string Currency { get; init; }

        //Factory Method
        //Encapsulation + Invarient ( Check business rules before create object)
        //Immutable
        public static Money Create(decimal amount, string currency)
        {
            if (amount < 0)
                throw new InvalidOperationException("Amount cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency required");

            return new Money(amount, currency);
        }

        public static Money Multiply(Money money, decimal factor)
        {
            if (factor < 0)
                throw new InvalidOperationException("Factor cannot be negative.");
            return new Money(money.Amount * factor, money.Currency);
        }
    }
}
