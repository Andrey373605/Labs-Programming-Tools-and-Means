using Martinvovich_353503_Lab5;
using Martinvovich_353503_Lab5.Martinovich_353503_Lab5.Domain;
using SerializerLib;

public class Program
{
    public static void Main(string[] args)
    {
        List<Airport> originalAirports = new List<Airport>
        {
            new Airport
            {
                Name = "Airport 1",
                Runways = new List<Airport.Runway>
                {
                    new Airport.Runway { RunwayName = "Runway 1.1" },
                    new Airport.Runway { RunwayName = "Runway 1.2" }
                }
            },
            new Airport
            {
                Name = "Airport 2",
                Runways = new List<Airport.Runway>
                {
                    new Airport.Runway { RunwayName = "Runway 2.1" }
                }
            }
        };
        foreach (var airport in originalAirports)
        {
            foreach (var way in airport.Runways)
            {
                Console.WriteLine($"{airport.Name}: {way.RunwayName}");
            }
        }

        Serializer serializer = new Serializer();
        var config = ConfigReader.LoadConfig();
        string fileName = config.FileName;

        serializer.SerializeByLINQ(originalAirports, $"{fileName}");
        var deserializedAirportsLINQ = serializer.DeSerializeByLINQ($"{fileName}");
        CheckEquality(originalAirports, deserializedAirportsLINQ, "LINQ-to-XML");

        serializer.SerializeXML(originalAirports, $"{fileName}");
        var deserializedAirportsXML = serializer.DeSerializeXML($"{fileName}");
        CheckEquality(originalAirports, deserializedAirportsXML, "XmlSerializer");

        serializer.SerializeJSON(originalAirports, $"{fileName}");
        var deserializedAirportsJSON = serializer.DeSerializeJSON($"{fileName}");
        CheckEquality(originalAirports, deserializedAirportsJSON, "Newtonsoft.Json");


    }
    static void CheckEquality(IEnumerable<Airport> original, IEnumerable<Airport> deserialized, string method)
    {
        bool equal = original.SequenceEqual(deserialized);
        Console.WriteLine($"{method} десериализация {(equal ? "совпадает" : "не совпадает")} с исходной коллекцией.");
    }
}
