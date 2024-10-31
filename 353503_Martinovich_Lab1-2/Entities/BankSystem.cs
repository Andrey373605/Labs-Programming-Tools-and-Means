using _353503_Martinovich_Lab1.Collections;

namespace _353503_Martinovich_Lab1.Entities
{
    internal class BankSystem
    {
        private delegate void HandlerClient(Client c);

        private delegate void HandlerDeposit(DepositRate d);

        public delegate void HandlerDepositToClient();

        public event HandlerDepositToClient DepositToClientEvent;

        private event HandlerClient ChangeClientEvent;

        private event HandlerDeposit ChangeDepositEvent;
        public MyCustomCollection<Client> Clients { get; }
        public MyCustomCollection<DepositRate> DepositRates { get; }

        private Journal journal;

        public BankSystem()
        {
            Clients = new MyCustomCollection<Client>();
            DepositRates = new MyCustomCollection<DepositRate>();
            journal = new Journal();
            ChangeClientEvent += journal.ChangeClientList;
            ChangeDepositEvent += journal.ChangeDepositList;
        }

        public void AddClient(string name)
        {
            Client client = new Client(name);
            Clients?.Add(client);
            ChangeClientEvent?.Invoke(client);

        }

        public void AddDepositRate(string name, double rate)
        {
            DepositRate depositRate = new DepositRate(name, rate);
            DepositRates?.Add(depositRate);
            ChangeDepositEvent?.Invoke(depositRate);
        }

        public void AddDepositToClient(string clientName, double amount, string depositTypeName)
        {
            Client client = GetClientByName(clientName);
            DepositRate depositRate = GetDepositTypeByName(depositTypeName);
            client.AddDeposit(new Deposit(amount, depositRate));
            DepositToClientEvent?.Invoke();
        }

        public void IncreaseClientDeposit(string clientName, double amount, string depositTypeName )
        {
            Clients.Reset();
            while (!Clients.CurrentIsNull())
            {
                if (Clients.Current().Name == clientName)
                {
                    Clients.Current().IncreaseDeposit(depositTypeName, amount);
                    break;
                }
                Clients.Next();
            }
        }

        private Client GetClientByName(string clientName)
        {
            Clients.Reset();
            while (!Clients.CurrentIsNull())
            {
                if (Clients.Current().Name == clientName)
                {
                    return Clients.Current();
                }
                Clients.Next();
            }
            throw new Exception("Client not found");
        }

        private DepositRate GetDepositTypeByName(string depositTypeName) 
        {
            DepositRates.Reset();
            while (!Clients.CurrentIsNull())
            {
                if (DepositRates.Current().DepositTypeName == depositTypeName)
                {
                    return DepositRates.Current();
                }
                DepositRates.Next();
            }
            throw new Exception("Deposit name not found");
        }

        public double GetClientSalvage(string clientName, string depositName)
        {
            try
            {
                return GetClientByName(clientName).GetSalvage(depositName);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public double CalculateTotalInterest()
        {
            Clients.Reset();
            double totalInterest = 0;
            while (!Clients.CurrentIsNull())
            {
                Client client = Clients.Current();
                client.Deposits.Reset();
                while (!client.Deposits.CurrentIsNull())
                {
                    totalInterest += client.Deposits.Current().CalculateInterest();
                    client.Deposits.Next();
                }
                Clients.Next();
            }

            return totalInterest;
        }

        public double CalculateClientInterest(string clientName)
        {
            double totalInterest = 0;
            Client? client = null;

            Clients.Reset();
            while (!Clients.CurrentIsNull())       
            {
                if (Clients.Current().Name == clientName)
                {
                    client = Clients.Current();
                }
                Clients.Next();
            }

            if (client == null)
            {
                return 0;
            }

            client.Deposits.Reset();
            while (!client.Deposits.CurrentIsNull())
            {
                totalInterest += client.Deposits.Current().CalculateInterest();
                client.Deposits.Next();
            }
            return totalInterest;
        }

        public double CalculateTotalDepositSize(string clientName)
        {
            DepositSize totalInterest = new DepositSize(0);
            Client? client = null;

            Clients.Reset();
            while (!Clients.CurrentIsNull())
            {
                if (Clients.Current().Name == clientName)
                {
                    client = Clients.Current();
                }
                Clients.Next();
            }

            if (client == null)
            {
                return 0;
            }

            client.Deposits.Reset();
            while (!client.Deposits.CurrentIsNull())
            {
                totalInterest = totalInterest + client.Deposits.Current().Amount;
                client.Deposits.Next();
            }
            return totalInterest.Amount;
        }
    }
}
