using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlannerLiFhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
    {
        Dictionary<string, int> searchList = new Dictionary<string, int>();

        public void LogRouteRequests(object sender, RouteRequestEventArgs e)
        {
            Console.WriteLine("test");

            if (searchList.ContainsKey(e.ToCity.Name))
            {
                searchList[e.ToCity.Name] += 1;
            }
            else
            {
                searchList.Add(e.ToCity.Name, 1);
            }

            Console.WriteLine("Current Request State");
            Console.WriteLine("---------------------");

            // foreach (ListKeyValuePair<string, int> entry in search)
            foreach (var entry in searchList)
            {
                Console.WriteLine("ToCity: " + entry.Key + " has been requested " + entry.Value + "time(s)");
            }
        }

        public int GetCityRequests(string city)
        {
            if (searchList.ContainsKey(city))
            {
                return searchList[city];
            }

            return 0;
        }
    }
}