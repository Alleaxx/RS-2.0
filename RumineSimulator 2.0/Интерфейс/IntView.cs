using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView
    {
        //Представление некоторых классов в графическом виде
        public GuiString classic_string { get; set; }
        public List<GuiString> all_properties = new List<GuiString>();
        public IntView()
        {


        }
        //Добавляем строковое свойство в список всех свойств
        public virtual void Add_Property(GuiString property)
        { Add_Property(all_properties, property); }

        public virtual void Add_Property(List<GuiString> list, GuiString property)
        {
            list.Add(property);
        }

    }
}
