using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
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
            Console.WriteLine("lat: {0} long: {1}", _latitude, _longitude);
        }

        public override string ToString()
        {
            string str = String.Format("{0}/{1}", Math.Round(Latitude, 2), Math.Round(Longitude, 2));
            string theName = "";

            if (Name != null)
                theName = Name + " ";

            return "WayPoint: " + theName + str;
        }

        public double Distance(WayPoint target)
        {
            return 6371 * Math.Acos(Math.Sin(DegToRad(this.Latitude)) * Math.Sin(DegToRad(target.Latitude)) + Math.Cos(DegToRad(this.Latitude)) * Math.Cos(DegToRad(target.Latitude)) * Math.Cos(DegToRad(target.Longitude - this.Longitude)));
        }

        // Lab 4, Aufgabe 2
        public static WayPoint operator +(WayPoint a, WayPoint b)
        {
            string name = a.Name;
            double latitude = a.Latitude + b.Latitude;
            double longitude = a.Longitude + b.Longitude;

            return new WayPoint(name, latitude, longitude);
        }

        // Lab 4, Aufgabe 2
        public static WayPoint operator -(WayPoint a, WayPoint b)
        {
            string name = a.Name;
            double latitude = a.Latitude - b.Latitude;
            double longitude = a.Longitude - b.Longitude;

            return new WayPoint(name, latitude, longitude);
        }

        private double DegToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

    }
}
