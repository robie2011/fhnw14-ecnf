using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter : IFormatter
    {
        private List<Type> supportedTypes = new List<Type>();
        private StringWriter stream;

        public SimpleObjectWriter(StringWriter stream)
        {
            this.stream = stream;

            supportedTypes.Add(typeof(double));
            supportedTypes.Add(typeof(int));
            supportedTypes.Add(typeof(string));

            // use the dot you must! - Yoda
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        public void Serialize(Stream s, object o)
        {
            //var propertyInfos2Serialize = GetPropertyInfos2Serialize(o);
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

        public void Next(Object o)
        {
            Type type = o.GetType();
            this.stream.WriteLine("Instance of " + type.FullName);

            var propertyInfos = type.GetProperties();

            foreach (var property in propertyInfos)
            {
                var value = property.GetValue(o);
                var name = property.Name;
                if (name.Equals("Index")) continue;

                if (supportedTypes.Contains(property.PropertyType))
                {
                    if(property.PropertyType == typeof(string)) {
                        this.stream.WriteLine(name + "=\"" + value + "\"");
                    } else {
                        this.stream.WriteLine(name + "=" + value);
                    }
                }
                else
                {
                    this.stream.WriteLine(name + " is a nested object...");
                    Next(property.GetValue(o));
                }
            }

            this.stream.WriteLine("End of instance");
        }
    
        public override string ToString()
        {
            return stream.ToString();
        }
    }
}