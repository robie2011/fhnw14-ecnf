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
    public static class LarsKessler
    {
        public static void Start()
        {
            var actions = new Action[3];

            for (var i = 0; i < actions.Length; i++)
            {
                actions[i] = () => Console.Write(i);
            }

            foreach (var a in actions)
            {
                a();
            }

            Console.ReadKey();
        }
    }
}
