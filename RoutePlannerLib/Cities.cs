﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using System.Diagnostics;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
   [Serializable]
    public class Cities
    {
        List<City> cityList = new List<City>();
        public int Count { get; private set; }

        // Lab 8, Aufgabe 1a, using System.Diagnostics added
        private static TraceSource source = new TraceSource("Cities");

       // Lab 9.1c
       private List<City> InitIndexForAlgorithm(List<City> foundCities)
        {
           // set index for FloydWarshall
           for (int index = 0; index < foundCities.Count; index++)
           {
               foundCities[index].Index = index;
           }
           return foundCities;
        }

        // Lab 2, Aufgabe 2b
        public int ReadCities(string filename)
        {

            source.TraceEvent(TraceEventType.Information, 42, "ReadCities started");            
			
			// set culture to english in order to avoid decimal errors by parsing strings to doubles
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            // Lab 4, Aufgabe 3
            int c = 0;

            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    var importedCitiesAsStrings = reader.GetSplittedLines('\t');
                    var importedCities = importedCitiesAsStrings.Select(cs => new City(cs[0].Trim(), cs[1].Trim(),
                                                    int.Parse(cs[2]),
                                                    double.Parse(cs[3]),
                                                    double.Parse(cs[4]))
                                                    ).ToList();
                    c = importedCities.Count();

                    cityList.AddRange(importedCities);
                }
            }
            catch(Exception e)
            {
                source.TraceEvent(TraceEventType.Critical, 42, "File not found: " + e.StackTrace);
            }
            Count += c;

            source.TraceEvent(TraceEventType.Information, 42, "ReadCities ended");
            source.Flush();
            return c;
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
            // Lab 6, Aufgabe 1
            return cityList
                .Where(c => location.Distance(c.Location) <= distance)
                .OrderBy(c => location.Distance(c.Location))
                .ToList();
        }

        // Lab 3, Aufgabe 1
        public City FindCity(string cityName)
        {
            var found = cityList.Find(city => city.Name.Equals(cityName));
            if (found != null) return found;
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
            return  InitIndexForAlgorithm(foundCities);
        }
        #endregion

       public List<City> FindCitiesBetween(string from, string to)
        {
            return FindCitiesBetween(FindCity(from), FindCity(to));
        }
    }
}