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
        public List<Interface_String> all_properties { get; set; }
        public InterfaceView()
        {


        }
        //Добавляем строковое свойство в список всех свойств
        public virtual void Add_Property(Interface_String property)
        { Add_Property(all_properties, property); }

        public virtual void Add_Property(List<Interface_String> list, Interface_String property)
        {
            list.Add(property);
        }

    }
}
