using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    public static class RobertRajakone
    {
        static Assembly assembly;
        static Cities cities;

        static RobertRajakone()
        {
            assembly = Assembly.GetExecutingAssembly();
            cities = new Cities();
            //cities.ReadCities("citiesTestDataLab2.txt");

        }

        public static void Start()
        {

            unterricht();
            Console.ReadKey();
        }

        static void unterricht()
        {
            var actions = new Action[3];
            for (var i = 0; i < actions.Length; i++)
            {
                //actions[i] = () => Console.Write(new iinteg i);
            }
            foreach (var a in actions)
            {
                a();
            }

        }


        static void TestsLab2()
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Console.WriteLine("Welcome to RoutePlanner ({0})", version);

            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            Console.WriteLine("{0}: {1}/{2}", wayPoint.Name, wayPoint.Latitude, wayPoint.Longitude);

            writeTitle("Lab2, Aufgabe 1a - Overwrite ToString()");
            Console.WriteLine(wayPoint.ToString());


            writeTitle("Lab2, Aufgabe 1b - Distanz Bern-Tripolis in KM");
            var Bern = new WayPoint("Bern", 46.9479222, 7.444608499999958);
            var Tripolis = new WayPoint("Tripolis", 32.8084124, 13.150967199999968);
            Console.WriteLine(Bern.Distance(Tripolis));

            writeTitle("Aufgabe 2a");
            var BernCity = new City("Bern", "Schweiz", 75000, 47.491823923434, 8.2121323123);

            writeTitle("Aufgabe 2b");
            var MyCities = new Cities();

            Console.WriteLine(MyCities.ReadCities("citiesTestDataLab2.txt"));
        }

        static void TestsLab5()
        {
            writeTitle("TestLab 5");
            IRoutes r = RoutesFactory.Create(cities);            
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
