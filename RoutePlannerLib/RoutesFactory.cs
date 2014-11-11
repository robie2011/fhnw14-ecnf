using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            var algorithmName = Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Properties.Settings.Default.RouteAlgorithm;
            return Create(cities, algorithmName);
        }

        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            IRoutes algorithm = null;
            Assembly asm = Assembly.GetExecutingAssembly();
            try
            {
                Type type = asm.GetType(algorithmClassName);
                algorithm = (IRoutes)Activator.CreateInstance(type,cities);
            }
            catch (Exception e) { }

            return algorithm;
        } 
    }
}
