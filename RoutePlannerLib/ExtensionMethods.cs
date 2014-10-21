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

    public static class LinqExtension
    {
        public static IEnumerable<T> SelectR<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {
            foreach (var element in list)
            {
                if(predicate.Invoke(element))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<K> Select2<T,K>(this IEnumerable<T> sequence, Func<T,K> transform)
        {
            foreach (var s in sequence)
                yield return transform(s);
        }

    }
}