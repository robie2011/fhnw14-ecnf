using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlannerLiFhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class City
    {
        public string Name {get; set;}
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
    }
}
