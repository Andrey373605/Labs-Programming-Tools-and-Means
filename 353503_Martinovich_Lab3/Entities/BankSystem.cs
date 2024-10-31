namespace _353503_Martinovich_Lab3.Entities
{
    internal class BankSystem
    {
        public delegate void HandlerClient(Client c);

        public delegate void HandlerDeposit(DepositRate d);

        public delegate void HandlerDepositToClient();

        public event HandlerDepositToClient DepositToClientEvent;

        public event HandlerClient ChangeClientEvent;

        public event HandlerDeposit ChangeDepositEvent;
        public List<Client> Clients { get; }
        public Dictionary<string,DepositRate> DepositRates { get; }

        public BankSystem()
        {
            Clients = new List<Client>();
            DepositRates = new Dictionary<string, DepositRate>();
        }

        public List<string> GetSortedTariffNames()
        {
            var query = from dr in DepositRates
                        orderby dr.Value.InterestRate descending
                        select dr.Key;

            return query.ToList();
        }

        public double GetTotalInterest()
        {
            return Clients.SelectMany(c => c.Deposits).Sum(d => d.CalculateInterest());
        }

        public double GetTotalDeposiSizet()
        {
            return Clients.SelectMany(c => c.Deposits).Sum(d => d.Amount);
        }

        public IEnumerable<Client> GetClients()
        {
            return Clients;
        }

        public string GetClientByMaxDeposit()
        {
            try
            {
                string? name = Clients.Select(c => new
                {
                    client = c,
                    percent = c.Deposits.Sum(d => d.Amount)
                })
                .OrderByDescending(c => c.percent)
                .FirstOrDefault()?.client.Name;
                return name;
            }
            catch
            {
                throw new Exception("No clients");
            }
        }

        public int CountClientThresHold(int threshold)
        {
            int count = Clients.Aggregate(0, (acc, client) =>
            {
                decimal totalInterest = client.Deposits.Sum(deposit => deposit.Amount);
                return totalInterest > threshold ? acc + 1 : acc;
            });
            return count;
        }

        public void GetDepositsByClient()
        {
            var clientSums = Clients
            .GroupBy(client => client.Name)
            .Select(group => new
            {
                Client = group.Key,
                TotalAmount = group.Sum(client => client.Deposits.Sum(deposit => deposit.Amount))
            })
            .ToList();

            foreach (var client in clientSums)
            {
                Console.WriteLine($"Client: {client.Client}, Total Amount: {client.TotalAmount}");
            }
        }

        public void AddClient(string name)
        {
            Client client = new Client(name);
            Clients?.Add(client);
            ChangeClientEvent?.Invoke(client);

        }

        public void AddDepositRate(string name, long rate)
        {
            DepositRate depositRate = new DepositRate(rate);
            DepositRates[name] = depositRate;
            ChangeDepositEvent?.Invoke(depositRate);
        }

        public void AddDepositToClient(string clientName, string depositName, long amount, string rateName)
        {
            Client? client = GetClientByName(clientName);
            DepositRate? depositRate = DepositRates[rateName];
            client.AddDeposit(new Deposit(amount, depositRate, depositName));
            DepositToClientEvent?.Invoke();
        }

        public void IncreaseClientDeposit(string clientName, long amount, string rateName)
        {
            Client? client = Clients.FirstOrDefault(x => x.Name == clientName);
            if (client != null)
            {
                client.IncreaseDeposit(rateName, amount);
            }
            throw new Exception("Client not found");
        }

        private Client GetClientByName(string clientName)
        {
            Client? client = Clients.FirstOrDefault(x => x.Name == clientName);
            if (client != null)
            {
                return client;
            }
            throw new Exception("Client not found");
        }

        private DepositRate GetDepositRateByName(string rateName)
        {
            DepositRate? rate = DepositRates[rateName];
            if (rate != null)
            {
                return rate;
            }
            throw new Exception("Rate name not found");
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

            double totalInterest = 0;
            foreach (var client in Clients)
            {
                foreach (var deposit in client.Deposits)
                {
                    totalInterest += deposit.CalculateInterest();
                }
            }

            return totalInterest;
        }

        public double CalculateClientInterest(string clientName)
        {
            double totalInterest = 0;
            Client? client = Clients.FirstOrDefault(x => x.Name == clientName);

            if (client != null)
            {
                foreach (var deposit in client.Deposits)
                {
                    totalInterest += deposit.CalculateInterest();

                }
                
            }
            return totalInterest;
        }

    }
}

