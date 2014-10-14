using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlannerLiFhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs : System.EventArgs
    {
        public City FromCity { get; set; }
        public City ToCity { get; set; }
        public TransportModes tM { get; set; }

        public RouteRequestEventArgs(City _fromCity, City _toCity, TransportModes _transportModes)
        {
            FromCity = _fromCity;
            ToCity = _toCity;
            tM = _transportModes;
        }
    }
}
