using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public static class TextReaderExtension
    {
        public static IEnumerable<string[]> GetSplittedLines(this TextReader r, char c)
        {
            String line;
            while ((line = r.ReadLine()) != null)
            {
                yield return line.Split(c);
            }
        }
    }
}