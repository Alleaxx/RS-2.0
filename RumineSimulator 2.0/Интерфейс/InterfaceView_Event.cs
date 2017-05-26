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
        public List<Interface_String> connectedEntities_properties = new List<Interface_String>();


        public InterfaceView_Event(Event eve) : base()
        {
            string_info = new Interface_String(eve.Name,"",true);
            event_properties.Add(new Interface_String("Основные свойства: ", "", true,true));
            event_properties.Add(new Interface_String("Дата: ", eve.date.ToShortTimeString(), true));
            event_properties.Add(new Interface_String("Длительность: ", eve.Duration.ToString(), true));
            event_properties.Add(new Interface_String("Конец: ", eve.date_end.ToShortTimeString(), true));
            event_properties.Add(new Interface_String("Тип: ", eve.EventType.ToString(), true));
            if(eve.Creator == null)
                event_properties.Add(new Interface_String("Неизвестно", "", true));
            else
                event_properties.Add(new Interface_String("Создатель: ", eve.Creator.Text, true));
            event_properties.Add(new Interface_String("Мод. дня: ", eve.next_day_mod.ToString(), true));
            event_properties.Add(new Interface_String("Мод. недели: ", eve.next_week_mod.ToString(), true));
            event_properties.Add(new Interface_String("Мод. месяца: ", eve.next_month_mod.ToString(), true));
            event_properties.Add(new Interface_String($"Реактивность({eve.Reasonable}): ", eve.Reaction.ToString(), true));
            connectedEntities_properties.Add(new Interface_String("Cписок участников: ", $"({eve.participants.Count})", false, true));
            for (int i = 0; i < eve.participants.Count; i++)
            {
                connectedEntities_properties.Add(new Interface_String(eve.participants.ElementAt(i).Key.nick, "", false,true));
                connectedEntities_properties.Add(new Interface_String(eve.participants.ElementAt(i).Value, "", true));
            }
            //Запись связанных событий
            if(eve.connected_events.Count != 0)
            {
                connectedEntities_properties.Add(new Interface_String("Связанные события", $"({eve.connected_events.Count})", false, true));
                for (int i = 0; i < eve.connected_events.Count; i++)
                {
                    connectedEntities_properties.Add(new Interface_String(eve.connected_events[i].Name, $"({eve.connected_events[i].id})", false));
                }
            }
            else
            {
                connectedEntities_properties.Add(new Interface_String("Связанных событий нет", "", false, true));
            }

        }
        public override void Add_Property(Interface_String property)
        {
            base.Add_Property(property);
            event_properties.Add(property);
        }
    }
}
