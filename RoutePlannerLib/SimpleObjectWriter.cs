using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter : IFormatter
    {
        private List<Type> supportedTypes;
        private StringWriter stream;

        public SimpleObjectWriter()
        {
            supportedTypes.Add(typeof(double));
            supportedTypes.Add(typeof(int));
            supportedTypes.Add(typeof(string));
        }

        public SimpleObjectWriter(StringWriter stream)
        {
            // TODO: Complete member initialization
            this.stream = stream;
            Console.WriteLine("custom constructor called");
        }

        public void Serialize(Stream s, object o)
        {

            var propertyInfos2Serialize = GetPropertyInfos2Serialize(o);
        }

        private List<PropertyInfo> GetPropertyInfos2Serialize(object o)
        {
            var propertyInfos2Serialize = new List<PropertyInfo>();
            Type type = o.GetType();
            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                if (supportedTypes.Contains(propertyInfo.PropertyType))
                {
                    propertyInfos2Serialize.Add(propertyInfo);
                }
            }

            return propertyInfos2Serialize;
        }

        public SerializationBinder Binder
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public StreamingContext Context
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public ISurrogateSelector SurrogateSelector
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override String ToString()
        {
            return "Test";
        }

        public void Next(City c)
        {

        }
    }
}
