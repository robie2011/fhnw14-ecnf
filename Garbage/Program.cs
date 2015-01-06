using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garbage
{
    // Lab 8, Aufgabe 2
    // Set StartupProject has to be changed within solution in order to run Garbage!
    class Program
    {
        private static byte[] b;
        private static int SIZE = 1000000;

        static void Main(string[] args)
        {
            // <gcServer enabled="true"/> ~ 0.475
            // <gcServer enabled="true"/> ~ 0.486
            Console.WriteLine(getGarbageTime());
            Console.ReadKey();
        }

        static long getGarbageTime()
        {
            int runs = 50;
            long time = 0;

            for (int i = 0; i < runs; i++)
            {
                time += generateGarbage();
            }

            return time / 50;
        }

        static long generateGarbage()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            for (int j = 0; j < 100; j++)
            {
                b = new byte[SIZE];
                for (int i = 0; i < SIZE; i++)
                {
                    b[i] = 42;
                }
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
