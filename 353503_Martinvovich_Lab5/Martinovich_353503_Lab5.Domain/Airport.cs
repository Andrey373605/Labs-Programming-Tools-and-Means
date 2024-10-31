using System;
using System.Collections.Generic;

namespace Martinvovich_353503_Lab5.Martinovich_353503_Lab5.Domain
{
    [Serializable]
    public class Airport : IEquatable<Airport>
    {
        public string Name { get; set; }
        public List<Runway> Runways { get; set; }

        public Airport(string name)
        {
            Name = name;
            Runways = new List<Runway>();
        }

        public Airport()
        {
            Runways = new List<Runway>();
        }

        public bool Equals(Airport other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Name == other.Name &&
                   Runways.SequenceEqual(other.Runways); ;
        }

        [Serializable]
        public class Runway : IEquatable<Runway>
        {
            public Runway(){}

            public Runway(string name) 
            {
                RunwayName = name;
            }

            public string RunwayName { get; set; }

            public bool Equals(Runway other)
            {
                if (other == null) return false;
                if (ReferenceEquals(this, other)) return true;

                return RunwayName == other.RunwayName;
            }
        }
    }
}
