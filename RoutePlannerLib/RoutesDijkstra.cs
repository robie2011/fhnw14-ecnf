using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesDijkstra : Routes
    {
        public RoutesDijkstra(Cities cities)
            : base(cities)
        {

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

        // Lab 4.1
        private List<Link> FindPath(List<City> citiesOnRoute, TransportModes mode)
        {
            var listOfLinks = new List<Link>();

            for (int i = 0; i < citiesOnRoute.Count - 1; i++)
            {
                listOfLinks.Add(FindLink(citiesOnRoute[i], citiesOnRoute[i + 1], mode));
            }

            return listOfLinks;
        }

        // Lab 4.1
        public Link FindLink(City fromCity, City toCity, TransportModes mode)
        {
            Link link = routes
                .Where(l => l.Equals(fromCity, toCity, mode) || l.Equals(toCity, fromCity, mode))
                .DefaultIfEmpty(null)
                .FirstOrDefault();

            if (link != null)
            {
                return new Link(fromCity, toCity, link.Distance);
            }
            else
            {
                return null;
            }
        }


        // Lab 4, Aufgabe 1
        // Lab04: Dijkstra implementation
        public override List<Link> FindShortestRouteBetween(City fromCity, City toCity, TransportModes mode)
        {
            return FindShortestRouteBetweenAlgorithm(fromCity, toCity, mode,null);
        }
        public List<Link> FindShortestRouteBetweenAlgorithm(City fromCity, City toCity, TransportModes mode, IProgress<string> progress = null)
        {
            report(progress, "Starting ");
            EmitRouteEvent(this, new RouteRequestEventArgs(fromCity, toCity, mode));

            // We can not make calculations if locations are 
            // We can not make calculations if no routes are available
            if (
                    fromCity.Location == null
                    || toCity.Location == null
                    || routes == null
                    || routes.Count < 1
                ) return null;


            

            var citiesBetween = cities.FindCitiesBetween(fromCity, toCity);
            report(progress, "FindCitiesBetween()");

            // We can not make calculations if there are no cities between
            if (citiesBetween == null || citiesBetween.Count < 1)
                return null;

            var source = citiesBetween[0];
            var target = citiesBetween[citiesBetween.Count - 1];

            Dictionary<City, double> dist;
            Dictionary<City, City> previous;
            var q = FillListOfNodes(citiesBetween, out dist, out previous);
            dist[source] = 0.0;
            report(progress, "FillListOfNodes()");

            // the actual algorithm
            previous = SearchShortestPath(mode, q, dist, previous);
            report(progress, "SearchShortestPath()");

            // create a list with all cities on the route
            var citiesOnRoute = GetCitiesOnRoute(source, target, previous);
            report(progress, "GetCitiesOnRoute()");

            // prepare final list if links
            var paths = FindPath(citiesOnRoute, mode);
            report(progress, "FindPath()");
            return paths;
        }

        private void report(IProgress<string> progress, string txt)
        {
            if (progress != null) progress.Report(txt + " done");
        }


        public Task<List<Link>> GoFindShortestRouteBetween(string p1, string p2, TransportModes transportModes, IProgress<string> progress = null)
        {
            City fromCity = cities.FindCity(p1) ?? new City(p1);
            City toCity = cities.FindCity(p2) ?? new City(p2);
            return Task.Run(() => FindShortestRouteBetweenAlgorithm(fromCity,toCity,transportModes, progress));
        }
    }
}
