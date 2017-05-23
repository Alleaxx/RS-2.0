using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_Event : InterfaceView
    {
        public List<Interface_String> event_properties = new List<Interface_String>();


        public InterfaceView_Event(Event eve) : base()
        {
            Interface_basic = new Interface_String(eve.Name,"",true);
            event_properties.Add(new Interface_String("Основные свойства", "", true));
            event_properties.Add(new Interface_String("Дата", eve.date.ToString(), false));
            event_properties.Add(new Interface_String("Тип", eve.EventType.ToString(), false));
            if(eve.Creator == null)
                event_properties.Add(new Interface_String("Неизвестно", "", false));
            else
                event_properties.Add(new Interface_String("Создатель", eve.Creator.Text, false));
            event_properties.Add(new Interface_String("Мод. дня", eve.next_day_mod.ToString(), false));
            event_properties.Add(new Interface_String("Мод. недели", eve.next_week_mod.ToString(), false));
            event_properties.Add(new Interface_String("Мод. месяца", eve.next_month_mod.ToString(), false));
        }
        public override void Add_Property(Interface_String property)
        {
            base.Add_Property(property);
            event_properties.Add(property);
        }
    }
}
