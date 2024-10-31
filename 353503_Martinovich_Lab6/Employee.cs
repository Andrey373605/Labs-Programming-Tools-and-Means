namespace _353503_Martinovich_lab6
{
    public class Employee
    {
        public int Age { get; set; }
        public bool Sex { get; set; }
        public string Name { get; set; } = string.Empty;

        public Employee(int age, bool sex, string name)
        {
            Age = age;
            Sex = sex;
            Name = name;
        }
    }
}
