namespace MasterDesignPattern.ResultPattern
{
    public class BankTransacation
    {
        public Result<decimal> Withdraw(Account account, decimal amount)
        {
            if (account.Balance < amount)
            {
                return Result<decimal>.Fail("Insufficient balance");
            }

            account.Debit(amount);

            return Result<decimal>.Ok(account.Balance); 
        }
    }
}
