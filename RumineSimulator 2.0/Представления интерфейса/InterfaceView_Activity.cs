using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_Activity : InterfaceView
    {
        public List<Interface_String> act_properties = new List<Interface_String>();

        public void Add_Property(Interface_String info)
        {
            act_properties.Add(info);
        }
    }
}
