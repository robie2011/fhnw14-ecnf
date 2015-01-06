using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using System.Diagnostics;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public abstract class Routes : IRoutes
    {
        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);
        public event RouteRequestHandler RouteRequestEvent;
        protected List<Link> routes = new List<Link>();
        protected Cities cities;
        public bool ExecuteParallel { get; set; }

        // Lab 8, Aufgabe 1a, using System.Diagnostics added
        private static TraceSource source = new TraceSource("Routes");

        public int Count
        {
            get { return routes.Count; }
        }

        protected void EmitRouteEvent(Object sender, RouteRequestEventArgs args)
        {
            if (RouteRequestEvent != null) RouteRequestEvent(sender, args);
        }


        /// <summary>
        /// Initializes the Routes with the cities.
        /// </summary>
        /// <param name="cities"></param>
        public Routes(Cities cities)
        {
            this.cities = cities;
            //this.rqw = new RouteRequestWatcher();
        }

        /// <summary>
        /// Reads a list of links from the given file.
        /// Reads only links where the cities exist.
        /// </summary>
        /// <param name="filename">name of links file</param>
        /// <returns>number of read route</returns>
        public int ReadRoutes(string filename)
        {
            source.TraceEvent(TraceEventType.Information, 42, "ReadRoutes started");

            using (TextReader reader = new StreamReader(filename))
            {
                IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                foreach (var route in reader.GetSplittedLines('\t'))
                {
                    City city1 = cities.FindCity(route[0]);
                    City city2 = cities.FindCity(route[1]);

                    // only add links, where the cities are found 
                    if ((city1 != null) && (city2 != null))
                    {
                        routes.Add(new Link(city1, city2, city1.Location.Distance(city2.Location),
                                                   TransportModes.Rail));
                    }
                }
            }

            source.TraceEvent(TraceEventType.Information, 42, "ReadRoutes ended");
            source.Flush();
            return Count;
        }

        // Lab 6.3
        public City[] FindCities(TransportModes transportMode)
        {
            List<City> cityList = new List<City>() ;
            routes
                .Where(lnk => lnk.TransportMode == transportMode)
                .ToList()
                .ForEach( lnk => {
                    cityList.Add(lnk.FromCity);
                    cityList.Add(lnk.ToCity);
                });

            return cityList.Distinct().ToArray();
        }

        public virtual List<Link> FindShortestRouteBetween(string from, string to, TransportModes mode)
        {
            // created in order to get it to work
            City fromCity = cities.FindCity(from) ?? new City(from);
            City toCity = cities.FindCity(to) ?? new City(to);

            return FindShortestRouteBetween(fromCity, toCity, mode);
        }

        public abstract List<Link> FindShortestRouteBetween(City fromCity, City toCity, TransportModes mode);
        

    }
}