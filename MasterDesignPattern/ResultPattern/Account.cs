
namespace MasterDesignPattern.ResultPattern
{
    public class Account
    {
        public decimal Balance { get; internal set; }

        internal decimal Debit(decimal amount)
        {
            return Balance = Balance - amount;
        }
    }
}