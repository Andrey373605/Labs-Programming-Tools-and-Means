

namespace _353503_Martinovich_Lab3.Entities
{
    internal class Client
    {
        public string Name { get; set; }
        public List<Deposit> Deposits { get; }

        public Client(string name)
        {
            Name = name;
            Deposits = new List<Deposit>();
        }

        public void IncreaseDeposit(string rateName, long amount)
        {
            Deposit? deposit = Deposits.FirstOrDefault(x => x.Name == rateName);
            if (deposit != null)
            {
                deposit?.IncreaseDeposit(amount);
                return;
            }
            throw new Exception("Deposit not found");
        }

        public double GetSalvage(string rateName)
        {
            Deposit? deposit = Deposits.FirstOrDefault(x => x.Name == rateName);
            if (deposit != null)
            {
                return deposit.CalculateInterest();
            }
            throw new Exception("Deposit not found");
        }

        public void AddDeposit(Deposit deposit)
        {
            Deposits.Add(deposit);
        }
    }
}

