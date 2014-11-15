using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Dynamic
{
    public class World : DynamicObject
    {
        private Cities cities;

        public World(Cities cities)
        {
            this.cities = cities;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = this.cities.FindCity(binder.Name);
            if (result == null)
            {
                result = String.Format("The city \"{0}\" does not exist!", binder.Name);
            }
            
            return true;
        }

    }
}
