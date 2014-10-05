
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{

    public class RouteRequestEventArgs : EventArgs
    {        
        public City FromCity {get; private set;}
        public City ToCity { get; private set; }
        public TransportModes Mode { get; private set; }

        public RouteRequestEventArgs(string from, string to, TransportModes mode)
        {
            FromCity = new City(from);
            ToCity = new City(to);
            Mode = mode;
        }
    }

    public class RouteRequestWatcher
    {
        Dictionary<string,int> Data = new Dictionary<string,int>();
        public Routes Routes;

        public int GetCityRequests(string cityName)
        {
            int r = 0;
            if (Data.ContainsKey(cityName))
            { r = Data[cityName]; }
                
            return r;
        }

        // Lab 3, Aufgabe 2c
        public void LogRouteRequests(object sender, RouteRequestEventArgs e)
        {
            if (Data.ContainsKey(e.ToCity.Name))
            { Data[e.ToCity.Name]++; }

        }
    }

    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public class Routes
    {
        // Lab 3, Aufgabe 2a
        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);
        public event RouteRequestHandler RouteRequestEvent;

        List<Link> routes = new List<Link>();
        Cities cities;

        public int Count
        {
            get { return routes.Count; }
        }

        /// <summary>
        /// Initializes the Routes with the cities.
        /// </summary>
        /// <param name="cities"></param>
        public Routes(Cities cities)
        {
            this.cities = cities;
        }

        /// <summary>
        /// Reads a list of links from the given file.
        /// Reads only links where the cities exist.
        /// </summary>
        /// <param name="filename">name of links file</param>
        /// <returns>number of read route</returns>
        public int ReadRoutes(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var linkAsString = line.Split('\t');

                    City city1 = cities.FindCity(linkAsString[0]);
                    City city2 = cities.FindCity(linkAsString[1]);

                    // only add links, where the cities are found 
                    if ((city1 != null) && (city2 != null))
                    {
                        routes.Add(new Link(city1, city2, city1.Location.Distance(city2.Location),
                                                   TransportModes.Rail));
                    }
                }
            }

            return Count;

        }

        public List<Link> FindShortestRouteBetween(string fromCity, string toCity,
                                        TransportModes mode)
        {
            var result = new List<Link>();
            //TODO: Implement

            // Lab 3, Aufgabe 2b
            var e = new RouteRequestEventArgs(fromCity, toCity, mode);
            notifyRouteRequestListener(e);
            return result;
        }

        // Lab 3, Aufgabe 2b
        void notifyRouteRequestListener(RouteRequestEventArgs e)
        {
            if (RouteRequestEvent != null)
            {
                RouteRequestEvent(this, e);
            }
        }

    }
}
