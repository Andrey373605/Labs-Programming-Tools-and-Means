using Martinvovich_353503_Lab5.Martinovich_353503_Lab5.Domain;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SerializerLib
{
    public class Serializer : ISerializer
    {
        public IEnumerable<Airport> DeSerializeByLINQ(string fileName)
        {
            XElement xml = XElement.Load(fileName);
            return from airport in xml.Descendants("Airport")
                   select new Airport
                   {
                       Name = airport.Element("Name").Value,
                       Runways = (from runway in airport.Element("Runways").Elements("Runway")
                                  select new Airport.Runway
                                  {
                                      RunwayName = runway.Attribute("Name").Value
                                  }).ToList()
                   };
        }

        public IEnumerable<Airport> DeSerializeJSON(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<Airport>>(json) ?? new List<Airport>(); ;
        }

        public IEnumerable<Airport> DeSerializeXML(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Airport>));
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return (List<Airport>)(serializer.Deserialize(fs) ?? new List<Airport>());
            }
        }

        public void SerializeByLINQ(IEnumerable<Airport> airports, string fileName)
        {
            XElement xml = new XElement("Airports",
            new XAttribute("Count", airports.Count()),
            from port in airports
            select new XElement("Airport",
                new XElement("Name", port.Name),
                new XElement("Runways",
                    from way in port.Runways
                    select new XElement("Runway",
                        new XAttribute("Name", way.RunwayName)
                    )
                )
            )
        );
            xml.Save(fileName);
        }

        public void SerializeJSON(IEnumerable<Airport> airports, string fileName)
        {
            string json = JsonConvert.SerializeObject(airports, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public void SerializeXML(IEnumerable<Airport> airports, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Airport>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fs, airports);
            }
        }
    }
}
