
namespace _353503_Martinovich_Lab1.Entities
{
    internal class Journal
    {
        public Journal() { }
        public void ChangeClientList(Client c)
        {
            Console.WriteLine($"Был добавлен клиент {c.Name}");
        }

        public void ChangeDepositList(DepositRate d)
        {
            Console.WriteLine($"Был добавлен депозит {d.DepositTypeName} с ставкой {d.InterestRate}%");
        }


    }
}
