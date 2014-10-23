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
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (username.Contains("Rajakone"))
            {
                RobertRajakone.Start();
            }
            else
            {
                LarsKessler.Start();
            }
        }
    }
}
