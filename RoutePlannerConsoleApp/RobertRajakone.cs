using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using System.IO;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{

    public static class RobertRajakone
    {

        public static class TaskTests
        {
            public static void start()
            {
                var alltask = Task.WhenAll( 
                    downloadSourceUrl("http://www.fhnw.ch"), 
                    downloadSourceUrl("http://www.google.ch"), 
                    downloadSourceUrl("http://www.digitec.ch")
                    );

            }

            static Task<string> downloadSourceUrl(string url)
            {
                return Task.Run(() =>
                    {
                        Console.WriteLine("Start: " + url);
                        var client = new System.Net.WebClient();
                        var s = client.DownloadString(url);
                        Console.WriteLine("Download done: " + url);
                        return s;
                    }
                );
            }
        }

        public static class Parallism
        {
            public static void CalculatePrimeSingle( int from, int to)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                var numbers = Enumerable.Range(from, to);
                var parallelQuery = numbers.Where(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i != 0));

                stopWatch.Stop();
                foreach (var i in parallelQuery.ToArray()) Console.WriteLine(i);
                Console.WriteLine( String.Format(" {0} - {1}, Single, Eslapsed Ticks: {2} ", from,to, stopWatch.ElapsedTicks));
            }
            public static void CalculatePrimeParallel(int from, int to)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                
                var numbers = Enumerable.Range(from, to);
                var parallelQuery = numbers.AsParallel().Where(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i != 0));

                stopWatch.Stop();
                foreach (var i in parallelQuery.ToArray().OrderBy(i=>i)) Console.WriteLine(i);
                Console.WriteLine(String.Format(" {0} - {1}, Single, Eslapsed Ticks: {2} ", from, to, stopWatch.ElapsedTicks));
            }

            public static void CheckFloyedWarshallAlg()
            {
                Cities cities = new Cities();

                cities.ReadCities(@"C:\data\sourcecode\fhnw\ecnf\fhnw14-ecnf\RoutePlannerTest\data\citiesTestDataLab11.txt");
                /*
                Routes routes = new RoutesDijkstra(cities);
                long dijkstraTime = FindRoutes(routes);
                */

                var routes = new RoutesFloydWarshall(cities);
                routes.ExecuteParallel = true;
                long floydWarshallTime = FindRoutes(routes);
            }

            private static long FindRoutes(Routes routes)
            {
                int count = routes.ReadRoutes(@"C:\data\sourcecode\fhnw\ecnf\fhnw14-ecnf\RoutePlannerTest\data\linksTestDataLab11.txt");

                // test available cities
                Stopwatch timer = new Stopwatch();

                timer.Start();
                List<Link> links = routes.FindShortestRouteBetween("Lyon", "Berlin", TransportModes.Rail);
                return timer.ElapsedTicks;
            }
        }

        static RobertRajakone()
        {

        }


        public static void lektion08_parallelism()
        {

            Parallism.CalculatePrimeSingle(3, 30);
            Parallism.CalculatePrimeParallel(3, 30);
        }

        public static void excel()
        {
            
            var excelFileName = @"C:\tmp\out.xlsx";
            System.Console.WriteLine(excelFileName);

            var bern = new City("Bern", "Switzerland", 5000, 46.95, 7.44);
            var zuerich = new City("Zürich", "Switzerland", 100000, 32.876174, 13.187507);
            var aarau = new City("Aarau", "Switzerland", 10000, 35.876174, 12.187507);
            var link1 = new Link(bern, aarau, 15, TransportModes.Ship);
            var link2 = new Link(aarau, zuerich, 20, TransportModes.Ship);
            var links = new List<Link>();
            links.Add(link1);
            links.Add(link2);

            var excel = new ExcelExchange();


            excel.WriteToFile(excelFileName, bern, zuerich, links);
        }

        public static void Start()
        {
            Parallism.CheckFloyedWarshallAlg();
            
            //Console.ReadKey();

        }

        static void serialTests()
        {
            var file = @"C:\coding\ECNF\RoutePlaner\RoutePlannerTest\data\citiesTestDataLab2.txt";
            var cities = new Cities();
            cities.ReadCities(file);

            var bkpFile = @"c:\tmp\serialize.txt";
            var stream = new FileStream(bkpFile, FileMode.Create, FileAccess.Write, FileShare.None);
            var formatter = new SimpleIniFormatter();
            formatter.Serialize(stream, cities);
        }

        static void unterricht6_12()
        {
            int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var n = new List<int>(numbers);
            var l = n.SelectR( p => p < 3 ).ToList();
            l.ForEach( i => Console.WriteLine(i) );
            
        }

        static void unterricht6_1()
        {
            var actions = new Action[3];
            for (var i = 0; i < actions.Length; i++)
            {
                var x = i;
                actions[i] = () => Console.Write(x);
            }
            foreach (var a in actions)
            {
                a();
            }

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
