using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView
    {
        //Представление некоторых классов в графическом виде
        public Interface_String string_info { get; set; }
        public InterfaceView()
        {


        }
        public virtual void Add_Property(Interface_String property, bool spec = true)
        { }
    }
}
