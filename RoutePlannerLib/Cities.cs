using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        List<City> Data = new List<City>();
        public int Count { get; private set; }

        // Lab 2, Aufgabe 2b
        public int ReadCities(string filename)
        {
            int r = 0;
            using (StreamReader sr = File.OpenText(filename))
            {
                while(!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] obj = s.Split('\t');
                    Data.Add(new City(
                        obj[0], 
                        obj[1], 
                        int.Parse(obj[2]), 
                        double.Parse(obj[3]), 
                        double.Parse(obj[4])) 
                        );
                    ++r;
                }
            }
            Count += r;
            return r;
        }

        // Lab 2, Aufgabe 2c
        public City this[int i]
        {
            get {
                if (i >= Count) return null;
                else return Data[i]; 
            }
        }

        // Lab 2, Aufgabe 2d
        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            var Results = new List<City>();
            foreach (var c in Data)
            {
                if (location.Distance(c.Location) <= distance)
                    Results.Add(c);
            }

            // Lab 2, Aufgabe 2e
            Results.Sort(delegate(City c1, City c2)
            {
                double d1 = location.Distance(c1.Location);
                double d2 = location.Distance(c2.Location);
                return d1.CompareTo(d2);
            });

            return Results;
        }

        // Lab 3, Aufgabe 1
        public City FindCity(string cityName)
        {
            return Data.Find(delegate(City c)
            {
                return cityName.ToLower().Equals(c.Name.ToLower());
            });
        }

    }
}
