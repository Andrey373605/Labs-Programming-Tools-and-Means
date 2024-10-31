using System.Runtime.Serialization.Formatters.Binary;

namespace _353503_Martinovich_Lab4
{
    public class FileService : IFileService<Automobile>
    {
        public IEnumerable<Automobile> ReadFile(string fileName)
        {
            using var stream = File.OpenRead(fileName);
            var binReader = new BinaryReader(stream);
            while (stream.Position < stream.Length)
            {
                string name = "";
                int year = 0;
                bool is_new = false;
                try
                {
                    name = binReader.ReadString();
                    is_new = binReader.ReadBoolean();
                    year = binReader.ReadInt32(); 
                }
                catch (EndOfStreamException ex)
                {
                    Console.WriteLine($"Reached the end of the stream: {ex.Message}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"I/O error occurred: {ex.Message}");
                }
                catch (ObjectDisposedException ex)
                {
                    Console.WriteLine($"Stream was closed unexpectedly: {ex.Message}");
                }

                yield return new Automobile(name, is_new, year);
            }
        }

        public void SaveData(IEnumerable<Automobile> data, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using var stream = File.Create(fileName);
                var binWriter = new BinaryWriter(stream);
                foreach (var participant in data)
                {
                    binWriter.Write(participant.Name);
                    binWriter.Write(participant.IsNew);
                    binWriter.Write(participant.Year);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error occurred: {ex.Message}");
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"Stream was closed unexpectedly: {ex.Message}");
            }
        }
    }
}
