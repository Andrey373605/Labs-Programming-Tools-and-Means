using static System.Reflection.Metadata.BlobBuilder;

namespace _353503_Martinovich_Lab4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //task 1
            string folderName = "Martinovich_Lab4";
            string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (Directory.Exists(path))
            {
                Console.WriteLine($"Папка {folderName} удалена");
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(path);
            Console.WriteLine($"Папка {folderName} успешно создана по пути: {path}");

            //task 2
            string[] extensions = { ".txt", ".rtf", ".dat", ".inf" };
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                string fileName = Path.GetRandomFileName();
                string extension = extensions[random.Next(extensions.Length)];
                string fullPath = Path.Combine(path, Path.ChangeExtension(fileName, extension));

                using (File.Create(fullPath)) { }
            }

            //task 3
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string extension = Path.GetExtension(file);
                Console.WriteLine($"Файл: {fileName} имеет расширение {extension}");
            }

            //task 4
            List<Automobile> automobiles = new List<Automobile>()
            {
                new Automobile("Audi", false, 1999),
                new Automobile("Honda", true, 2011),
                new Automobile("Reno", false, 2015),
                new Automobile("BMW", true, 2009),
                new Automobile("Porshe", false, 2003),
                new Automobile("Toyota", true, 1998),
            };

            //task 5
            

            var fileService = new FileService();

            string relativeOldFileName = "oldFileName.dat";
            string relativeNewFileName = "newFileName.dat";

            string oldFileName = Path.GetFullPath(relativeOldFileName);
            string newFileName = Path.GetFullPath(relativeNewFileName);

            fileService.SaveData(automobiles, oldFileName);

            if (File.Exists(newFileName))
            {
                File.Delete(newFileName);
            }

            //task 6
            if (File.Exists(oldFileName))
            {
                File.Move(oldFileName, newFileName);
                Console.WriteLine($"Файл переименован в {newFileName}");
            }
            else
            {
                Console.WriteLine($"Файл {oldFileName} не найден.");
            }

            //task 7
            List<Automobile> automobiles2 = new List<Automobile>();
            foreach (var a in fileService.ReadFile(newFileName))
            {
                automobiles2.Add(a);
            }
            automobiles2.Sort(new MyCustomComparer<Automobile>());

            //task 8
            foreach (var a in automobiles)
            {
                Console.WriteLine(a.Name);
            }

            Console.WriteLine();

            foreach (var a in automobiles2)
            {
                Console.WriteLine(a.Name);
            }

            //task 9

            var sorted_auto = automobiles2.OrderBy(x => x.Year).ToList();
            foreach (var a in sorted_auto)
            {
                Console.WriteLine($"название: {a.Name} Год: {a.Year}" );
            }

        }
    }


}
