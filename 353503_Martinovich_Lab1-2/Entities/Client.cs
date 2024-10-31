using _353503_Martinovich_Lab1.Collections;

namespace _353503_Martinovich_Lab1.Entities
{
    internal class Client
    {
        public string Name { get; set; }
        public MyCustomCollection<Deposit> Deposits { get; }

        public Client(string name)
        {
            Name = name;
            Deposits = new MyCustomCollection<Deposit>();
        }

        public void IncreaseDeposit(string depositName, double amount)
        {
            Deposits.Reset();
            while (!Deposits.CurrentIsNull())
            {
                if (Deposits.Current().Rate.DepositTypeName == depositName)
                {
                    Deposits.Current().Amount.IncreaseDepositSize(amount);
                    break;
                }
                Deposits.Next();
            }
        }

        public double GetSalvage(string depositName)
        {
            Deposits.Reset();
            while (!Deposits.CurrentIsNull())
            {
                if (Deposits.Current().Rate.DepositTypeName == depositName)
                {
                    Deposits.Current().CalculateInterest();
                    break;
                }
                Deposits.Next();
            }
            return 0;
        }

        public void AddDeposit(Deposit deposit)
        {
            Deposits.Add(deposit);
        }
    }
}
