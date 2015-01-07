using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    [Serializable]
    public class City
    {
        [XmlIgnore]
        public int Index { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public WayPoint Location { get; set; }

        // Lab 2, Aufgabe a
        public City(string name, string country, int population, double lat, double lng)
        {
            Name = name;
            Country = country;
            Location = new WayPoint(name, lat, lng);
            Population = population;
        }

        public City(string name)
        {
            Name = name;
        }

        public Boolean Equals(Object o)
        {
            var c = o as City;
            return c.Name.Equals(this.Name) && c.Country.Equals(this.Country);
        }
    }
}