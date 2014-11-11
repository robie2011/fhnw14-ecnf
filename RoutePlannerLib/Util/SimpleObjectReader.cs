using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections.Specialized;
namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectReader
    {
        readonly StringReader reader;
        StreamingContext context;

        public SimpleObjectReader(StringReader sb)
        {
            reader = sb;
            context = new StreamingContext(StreamingContextStates.All);

            // use the dot you must! - Yoda
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        public City Next()
        {
            return (City) readInstance();
        }

        object readInstance()
        {
            // Get Type from serialized data
            var className = reader.ReadLine().Replace("Instance of ","");
            Type type = Type.GetType(className);

            // Create object of just found type name
            Object obj = FormatterServices.GetUninitializedObject(type);

            // Get type members
            MemberInfo[] members = FormatterServices.GetSerializableMembers(obj.GetType(), context);

            // create data array for each member
            var data = new object[members.Length];

            // store serialized varible name -> value pairs
            var fieldname2value = new Dictionary<string,object>();

            // read attributes
            string line;
            while ( (line = reader.ReadLine()).Length>0 && !line.Equals("End of instance") )
            {
                bool isNestedObject = line.Contains("is a nested object...");   
                if(isNestedObject)
                {
                    var objectName = line.Replace(" is a nested object...", "");
                    var objectValue = readInstance();
                    fieldname2value.Add(objectName, objectValue);
                }
                else
                {
                    var attributeData=line.Split('=');
                    var attributeName = attributeData[0];
                    var attributeValue = attributeData[1];
                    fieldname2value[attributeName] = attributeValue;
                }

            }

            for (int i = 0; i < members.Length; ++i)
            {
                FieldInfo fieldInfo = (FieldInfo)members[i];
                string actualName = fieldInfo.Name;
                if (actualName.Contains(">k__BackingField"))
                {
                    actualName = actualName.Replace(">k__BackingField", "");
                    actualName = actualName.Replace("<", "");
                }

                if (fieldname2value.ContainsKey(actualName))
                {
                    var fieldValue = fieldname2value[actualName];
                    if (fieldInfo.FieldType.Name.Equals( typeof(string).Name))
                    {
                        fieldValue = ((string)fieldValue).Replace("\"", "");
                    }
                    data[i] = System.Convert.ChangeType(fieldValue, fieldInfo.FieldType);
                }else
                {
                    throw new SerializationException("Missing field value : " + actualName);
                }
            }

            // Populate object members with theri values and return object.
            return FormatterServices.PopulateObjectMembers(obj, members, data);
        }
    }
}
