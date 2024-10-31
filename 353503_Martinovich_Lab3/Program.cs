namespace _353503_Martinovich_Lab3.Entities
{
    public class Program
    {
        static int Main()
        {
            Solve();
            return 0;
        }

        static void Solve()
        {
            BankSystem bank_system = new BankSystem();

            bank_system.AddClient("Andrey");
            bank_system.AddClient("Artem");

            bank_system.AddDepositRate("Super", 5);
            bank_system.AddDepositRate("Light", 2);

            bank_system.DepositToClientEvent += () => Console.WriteLine("Был добавлне депозит клиенту");    

            bank_system.AddDepositToClient("Andrey", "AndreyDeposit", 100, "Super");
            bank_system.AddDepositToClient("Artem", "Artem Deposit", 50, "Light");


            bank_system.GetDepositsByClient();
            //foreach (var item in bank_system.GetSortedTariffNames())
            //{
            //    Console.WriteLine(item);
            //}
            //double a = bank_system.CalculateTotalInterest();
            //Console.WriteLine(a);

        }

    }
}
