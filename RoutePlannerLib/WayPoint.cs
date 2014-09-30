using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlannerLiFhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name;
            Latitude = _latitude;
            Longitude = _longitude;
        }

        public override string ToString()
        {
            string str = String.Format("{0}/{1}", Math.Round(Longitude, 2), Math.Round(Latitude, 2));
            if (Name.Length > 0)
                str = Name + " " + str;
            return str;
        }
    }
}
