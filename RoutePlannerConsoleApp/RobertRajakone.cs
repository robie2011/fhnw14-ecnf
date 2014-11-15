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


        static RobertRajakone()
        {
            //assembly = Assembly.GetExecutingAssembly();
            //cities = new Cities();
            //cities.ReadCities("citiesTestDataLab2.txt");
            ;

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

            //excel();
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
