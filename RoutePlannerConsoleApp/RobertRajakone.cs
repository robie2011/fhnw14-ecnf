﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RobertRajakone
    {
        public static void main(string[] args)
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
            var Bern = new WayPoint("Bern", 46.9479222, 7.444608499999958);
            var Tripolis = new WayPoint("Tripolis", 32.8084124, 13.150967199999968);
            Console.WriteLine(Bern.Distance(Tripolis));

            writeTitle("Aufgabe 2a");
            var BernCity = new City("Bern", "Schweiz", 75000, 47.491823923434, 8.2121323123);

            writeTitle("Aufgabe 2b");
            var MyCities = new Cities();

            Console.WriteLine(MyCities.ReadCities("citiesTestDataLab2.txt"));

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