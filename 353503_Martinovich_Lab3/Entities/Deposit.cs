namespace _353503_Martinovich_Lab3.Entities
{
    internal class Deposit
    {
        private long _amount;
        public long Amount { get => _amount; }
        public DepositRate Rate { get; }

        public string Name { get; }

        public Deposit(long amount, DepositRate rate, string name)
        {
            _amount = amount;
            Rate = rate;
            Name = name;
        }
        public double CalculateInterest()
        {
            return Amount * Rate.InterestRate / 100;
        }

        public void IncreaseDeposit(long amount)
        {
            _amount += amount;
        }
    }
}
