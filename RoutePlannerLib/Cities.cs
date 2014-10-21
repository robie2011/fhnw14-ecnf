using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        List<City> cityList = new List<City>();
        public int Count { get; private set; }

        // Lab 2, Aufgabe 2b
        public int ReadCities(string filename)
        {
            // set culture to english in order to avoid decimal errors by parsing strings to doubles
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            int r = 0;

            // Lab 4, Aufgabe 3
            using (TextReader reader = new StreamReader(filename))
            {
                IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');
                foreach (var cs in reader.GetSplittedLines('\t'))
                {
                    cityList.Add(new City(cs[0].Trim(), cs[1].Trim(),
                                                int.Parse(cs[2]),
                                                double.Parse(cs[3]),
                                                double.Parse(cs[4])));
                    r++;
                }
            }

            /*
            int r = 0;
            using (StreamReader sr = File.OpenText(filename))
            {
                while(!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] obj = s.Split('\t');

                    cityList.Add(new City(
                        obj[0], 
                        obj[1], 
                        int.Parse(obj[2]), 
                        Double.Parse(obj[3]), 
                        Double.Parse(obj[4]))
                    );
                    ++r;
                }
            }
            */
            Count += r;
            return r;
        }

        // Lab 2, Aufgabe 2c
        public City this[int i]
        {
            get
            {
                if (i >= Count) return null;
                else return cityList[i];
            }
        }

        // Lab 2, Aufgabe 2d
        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            var Results = new List<City>();
            foreach (var c in cityList)
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
            return cityList.Find(city => city.Name.ToLower().Equals(cityName.ToLower()));
        }

        #region Lab04: FindShortestPath helper function
        /// <summary>
        /// Find all cities between 2 cities 
        /// </summary>
        /// <param name="from">source city</param>
        /// <param name="to">target city</param>
        /// <returns>list of cities</returns>
        public List<City> FindCitiesBetween(City from, City to)
        {
            var foundCities = new List<City>();
            if (from == null || to == null)
                return foundCities;

            foundCities.Add(from);

            var minLat = Math.Min(from.Location.Latitude, to.Location.Latitude);
            var maxLat = Math.Max(from.Location.Latitude, to.Location.Latitude);
            var minLon = Math.Min(from.Location.Longitude, to.Location.Longitude);
            var maxLon = Math.Max(from.Location.Longitude, to.Location.Longitude);

            // renames the name of the "cities" variable to your name of the internal City-List
            foundCities.AddRange(cityList.FindAll(c =>
                c.Location.Latitude > minLat && c.Location.Latitude < maxLat
                        && c.Location.Longitude > minLon && c.Location.Longitude < maxLon));

            foundCities.Add(to);
            return foundCities;
        }
        #endregion
    }
}