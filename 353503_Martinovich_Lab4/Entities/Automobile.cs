namespace _353503_Martinovich_Lab4
{
    public class Automobile
    {
        public Automobile(string name, bool isNew, int year)
        {
            Name = name;
            IsNew = isNew;
            Year = year;
        }


        public string Name { get; set; }
        public bool IsNew { get; set;  }
        public int Year { get; set; }

    }
}
