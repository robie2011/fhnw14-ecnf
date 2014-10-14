using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using RoutePlannerLiFhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public class Routes
    {
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


                /*
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
                 */
            }
            return Count;
        }

        /*
        public List<Link> FindShortestRouteBetween(string fromCity, string toCity,
                                TransportModes mode)
        {
            City fCity = new City(fromCity);
            City tCity = new City(toCity);

            RouteRequestEventArgs routeRequest = new RouteRequestEventArgs(fCity, tCity, mode);

            if (RouteRequestEvent != null)
            {
                RouteRequestEvent(this, routeRequest);
            }

            return new List<Link>();
        }
        */

        // Lab 4, Aufgabe 1
        #region Lab04: Dijkstra implementation
        public List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportModes mode)
        {
            // created in order to get it to work
            City fC = cities.FindCity(fromCity) ?? new City(fromCity);
            City tC = cities.FindCity(toCity) ?? new City(toCity);

            // call Event
            RouteRequestEventArgs routeRequest = new RouteRequestEventArgs(fC, tC, mode);

            if (RouteRequestEvent != null)
            {
                RouteRequestEvent(this, routeRequest);
            }

            if (fC.Location == null || tC.Location == null) return null; // EXIT IF THERE ARE NO ROUTES
            // canged to cities.FindCitiesBetween from FindCitiesBetween
            var citiesBetween = cities.FindCitiesBetween(fC, tC);
            if (citiesBetween == null || citiesBetween.Count < 1 || routes == null || routes.Count < 1)
                return null;

            var source = citiesBetween[0];
            var target = citiesBetween[citiesBetween.Count - 1];

            Dictionary<City, double> dist;
            Dictionary<City, City> previous;
            var q = FillListOfNodes(citiesBetween, out dist, out previous);
            dist[source] = 0.0;

            // the actual algorithm
            previous = SearchShortestPath(mode, q, dist, previous);

            // create a list with all cities on the route
            var citiesOnRoute = GetCitiesOnRoute(source, target, previous);



            // prepare final list if links
            return FindPath(citiesOnRoute, mode);
        }

        private static List<City> FillListOfNodes(List<City> cities, out Dictionary<City, double> dist, out Dictionary<City, City> previous)
        {
            var q = new List<City>(); // the set of all nodes (cities) in Graph ;
            dist = new Dictionary<City, double>();
            previous = new Dictionary<City, City>();
            foreach (var v in cities)
            {
                dist[v] = double.MaxValue;
                previous[v] = null;
                q.Add(v);
            }

            return q;
        }

        /// <summary>
        /// Searches the shortest path for cities and the given links
        /// </summary>
        /// <param name="mode">transportation mode</param>
        /// <param name="q"></param>
        /// <param name="dist"></param>
        /// <param name="previous"></param>
        /// <returns></returns>
        private Dictionary<City, City> SearchShortestPath(TransportModes mode, List<City> q, Dictionary<City, double> dist, Dictionary<City, City> previous)
        {
            while (q.Count > 0)
            {
                City u = null;
                var minDist = double.MaxValue;
                // find city u with smallest dist
                foreach (var c in q)
                    if (dist[c] < minDist)
                    {
                        u = c;
                        minDist = dist[c];
                    }

                if (u != null)
                {
                    q.Remove(u);
                    foreach (var n in FindNeighbours(u, mode))
                    {
                        var l = FindLink(u, n, mode);
                        var d = dist[u];
                        if (l != null)
                            d += l.Distance;
                        else
                            d += double.MaxValue;

                        if (dist.ContainsKey(n) && d < dist[n])
                        {
                            dist[n] = d;
                            previous[n] = u;
                        }
                    }
                }
                else
                    break;

            }

            return previous;
        }


        /// <summary>
        /// Finds all neighbor cities of a city. 
        /// </summary>
        /// <param name="city">source city</param>
        /// <param name="mode">transportation mode</param>
        /// <returns>list of neighbor cities</returns>
        private List<City> FindNeighbours(City city, TransportModes mode)
        {
            var neighbors = new List<City>();
            foreach (var r in routes)
                if (mode.Equals(r.TransportMode))
                {
                    if (city.Equals(r.FromCity))
                        neighbors.Add(r.ToCity);
                    else if (city.Equals(r.ToCity))
                        neighbors.Add(r.FromCity);
                }

            return neighbors;
        }

        private List<City> GetCitiesOnRoute(City source, City target, Dictionary<City, City> previous)
        {
            var citiesOnRoute = new List<City>();
            var cr = target;
            while (previous[cr] != null)
            {
                citiesOnRoute.Add(cr);
                cr = previous[cr];
            }
            citiesOnRoute.Add(source);

            citiesOnRoute.Reverse();
            return citiesOnRoute;
        }

        // Created Methods FindLink and FindPath
        private List<Link> FindPath(List<City> citiesOnRoute, TransportModes mode)
        {
            var listOfLinks = new List<Link>();

            for (int i = 0; i < routes.Count - 1; i++)
            {
                listOfLinks.Add(FindLink(cities[i], cities[i + 1], mode));
            }

            return listOfLinks;
        }

        public Link FindLink(City fromCity, City toCity, TransportModes mode)
        {
            foreach (Link l in routes)
            {
                if (l.Equals(fromCity, toCity, mode))
                {
                    return l;
                }
            }

            return null;
        }

        #endregion
    }
}