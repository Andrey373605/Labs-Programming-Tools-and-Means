using System.Reflection;

namespace _353503_Martinovich_lab6
{
    public class Program
    {
        static public void Main(string[] args)
        {
            string pathToDll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ClassLibrary1.dll");
            string pathToData = "employees.json";
            Assembly assembly = Assembly.LoadFrom(pathToDll);

            List<Employee> employees = new List<Employee>
            {
            new Employee(28, true, "John Doe"),
            new Employee(34, false, "Jane Smith"),
            new Employee(25, true, "Mike Johnson"),
            new Employee(30, false, "Emily Davis"),
            new Employee(29, true, "Tom Clark")
            };


            Type? fileServiceType = assembly.GetType("ClassLibrary1.FileService`1")?.MakeGenericType(typeof(Employee));
            if (fileServiceType == null)
            {
                throw new Exception("Не удалось найти тип 'ClassLibrary1.FileService`1'.");
            }
            var fileServiceInstance = Activator.CreateInstance(fileServiceType);

            MethodInfo? saveDataMethod = fileServiceType.GetMethod("SaveData");
            MethodInfo? readFileMethod = fileServiceType.GetMethod("ReadFile");
            if (readFileMethod == null || saveDataMethod == null)
            {
                throw new Exception("Не удалось найти тип метод");
            }

            saveDataMethod.Invoke(fileServiceInstance, new object[] { employees, pathToData });
            var result = readFileMethod.Invoke(fileServiceInstance, new object[] { "employees.json" });

            if (result is IEnumerable<Employee> loadedData)
            {
                Console.WriteLine("Данные, прочитанные из файла:");
                foreach (var employee in loadedData)
                {
                    Console.WriteLine($"Name: {employee.Name}, Age: {employee.Age}, Sex: {employee.Sex}");
                }
            }
            else
            {
                throw new InvalidOperationException("Ошибка при чтении данных: неверный формат или null значение.");
            }

        }
    }
}
