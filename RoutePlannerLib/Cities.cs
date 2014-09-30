using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RoutePlannerLiFhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        List<City> Data = new List<City>();

        public int ReadCities(string filename)
        {
            int r = 0;
            using (StreamReader sr = File.OpenText(filename))
            {
                while(!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] obj = s.Split('\t');
                    Data.Add(new City(
                        obj[0], 
                        obj[1], 
                        int.Parse(obj[2]), 
                        double.Parse(obj[3]), 
                        double.Parse(obj[4])) 
                        );
                    ++r;
                }
            }
            return r;
        }
    }
}
