using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_Activity : InterfaceView
    {
        public List<Interface_String> activity_properties = new List<Interface_String>();


        //Добавление строк в списки свойств активности
        public void Add_ActivityProperty(Interface_String info)
        {
            Add_Property(activity_properties, info);
        }
    }
}
