using System.Numerics;

namespace _353503_Martinovich_Lab1.Entities
{
    internal class DepositSize : IAdditionOperators<DepositSize, DepositSize, DepositSize>
    {
        private double _amount;
        public double Amount { get => _amount; }
        public DepositSize(double amount)
        {
            _amount = amount;
        }

        public void IncreaseDepositSize(double amount)
        {
            _amount += amount;
        }

        public static DepositSize operator+(DepositSize amount1, DepositSize amount2)
        {
            return new DepositSize(amount1.Amount + amount2.Amount);
        }
    }
}
