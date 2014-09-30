using RoutePlannerLiFhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Console.WriteLine("Welcome to RoutePlanner ({0})", version);

            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            Console.WriteLine("{0}: {1}/{2}", wayPoint.Name, wayPoint.Latitude, wayPoint.Longitude);

            writeTitle("Lab2, Aufgabe 1a - Overwrite ToString()");
            Console.WriteLine(wayPoint.ToString());


            writeTitle("Lab2, Aufgabe 1b - Distanz Bern-Tripolis in KM");
            var Bern = new WayPoint("Bern", 46.9479222,7.444608499999958 );
            var Tripolis = new WayPoint("Tripolis", 32.8084124, 13.150967199999968);
            Console.WriteLine(Bern.Distance(Tripolis));

            Console.ReadKey();
        }

        static void writeTitle(string s)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("========================================");
            Console.WriteLine(s);
        }
    }
}
