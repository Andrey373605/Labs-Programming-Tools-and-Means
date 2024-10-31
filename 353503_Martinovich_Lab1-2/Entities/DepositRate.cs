
namespace _353503_Martinovich_Lab1.Entities
{
    internal class DepositRate
    {
        public string DepositTypeName { get; set; }
        public double InterestRate {  get; set; }

        public DepositRate(string depositTypeName, double interestRate)
        {
            DepositTypeName = depositTypeName;
            InterestRate = interestRate;
        }
    }
}
