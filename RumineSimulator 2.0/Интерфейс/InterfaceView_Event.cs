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
        public List<Interface_String> participants_properties = new List<Interface_String>();


        public InterfaceView_Event(Event eve) : base()
        {
            Interface_basic = new Interface_String(eve.Name,"",true);
            event_properties.Add(new Interface_String("Основные свойства", "", true,true));
            event_properties.Add(new Interface_String("Дата", eve.date.ToShortTimeString(), true));
            event_properties.Add(new Interface_String("Тип", eve.EventType.ToString(), true));
            if(eve.Creator == null)
                event_properties.Add(new Interface_String("Неизвестно", "", true));
            else
                event_properties.Add(new Interface_String("Создатель", eve.Creator.Text, true));
            event_properties.Add(new Interface_String("Мод. дня", eve.next_day_mod.ToString(), true));
            event_properties.Add(new Interface_String("Мод. недели", eve.next_week_mod.ToString(), true));
            event_properties.Add(new Interface_String("Мод. месяца", eve.next_month_mod.ToString(), true));
            for (int i = 0; i < eve.participants.Count; i++)
            {
                participants_properties.Add(new Interface_String(eve.participants.ElementAt(i).Key.nick, eve.participants.ElementAt(i).Value, false));
            }
        }
        public override void Add_Property(Interface_String property)
        {
            base.Add_Property(property);
            event_properties.Add(property);
        }
    }
}
