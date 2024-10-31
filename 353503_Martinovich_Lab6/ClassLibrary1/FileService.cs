using System.IO;
using Newtonsoft.Json;


namespace ClassLibrary1
{
    public class FileService<T> : IFileService<T> where T : class
    {
        public IEnumerable<T> ReadFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException($"Файл {fileName} не найден");
            string jsonData = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData) ?? new List<T>();
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, jsonData);

        }
    }
}
