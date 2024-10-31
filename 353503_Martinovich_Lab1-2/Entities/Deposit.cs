namespace _353503_Martinovich_Lab1.Entities
{
    internal class Deposit
    {
        public DepositSize Amount { get; }
        public DepositRate Rate { get; }

        public Deposit(double amount, DepositRate rate)
        {
            Amount = new DepositSize(amount);
            Rate = rate;
        }
        public double CalculateInterest()
        {
            return Amount.Amount * Rate.InterestRate / 100;
        }
    }
}
