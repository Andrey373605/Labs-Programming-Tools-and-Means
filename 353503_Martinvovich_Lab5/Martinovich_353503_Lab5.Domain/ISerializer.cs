namespace Martinvovich_353503_Lab5.Martinovich_353503_Lab5.Domain
{
    public interface ISerializer
    {
        public IEnumerable<Airport> DeSerializeByLINQ(string fileName);
        public IEnumerable<Airport> DeSerializeXML(string fileName);
        public IEnumerable<Airport> DeSerializeJSON(string fileName);
        public void SerializeByLINQ(IEnumerable<Airport> airport, string fileName);
        public void SerializeXML(IEnumerable<Airport> airport, string fileName);
        public void SerializeJSON(IEnumerable<Airport> airport, string fileName);
    }
}
