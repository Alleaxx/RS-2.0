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
        public List<Interface_String> special_event_properties = new List<Interface_String>();
        public List<Interface_String> connectedEntities_properties = new List<Interface_String>();


        public InterfaceView_Event(Event eve) : base()
        {
            string_info = new Interface_String(eve.Name,"",true);
            Add_Property(new Interface_String("Основные свойства: ", "", true, true), false);
            Add_Property(new Interface_String("Дата: ", $"{eve.date.ToLongDateString()} {eve.date.ToShortTimeString()}", true), false);
            Add_Property(new Interface_String("Длительность: ", eve.Duration.ToString(), true), false);
            Add_Property(new Interface_String("Конец: ", $"{eve.date_end.ToLongDateString()} {eve.date_end.ToShortTimeString()}", true), false);
            Add_Property(new Interface_String("Тип: ", eve.EventType.ToString(), true), false);
            if(eve.Creator == null)
                Add_Property(new Interface_String("Неизвестно", "", true), false);
            else
                Add_Property(new Interface_String("Создатель: ", eve.Creator.Text, true), false);
            Add_Property(new Interface_String("Мод. дня: ", eve.next_day_mod.ToString(), true), false);
            Add_Property(new Interface_String("Мод. недели: ", eve.next_week_mod.ToString(), true), false);
            Add_Property(new Interface_String("Мод. месяца: ", eve.next_month_mod.ToString(), true), false);
            Add_Property(new Interface_String($"Реактивность({eve.Reasonable}): ", eve.Reaction.ToString(), true), false);
            Add_Property(new Interface_String($"Дней до удаления: ", eve.Days_Delete.ToString(), true), false);
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

            if (eve.EventGlobalType == EventType.historic)
            {
                HistoricEvent eve_history = (HistoricEvent)eve;
                for (int i = 0; i < eve_history.more_properties.Count; i++)
                {
                    Add_Property(new Interface_String(eve_history.more_properties.ElementAt(i).Key, eve_history.more_properties.ElementAt(i).Value,
                        true), false);
                }
            }

        }
        public override void Add_Property(Interface_String property,bool spec = true)
        {
            base.Add_Property(property);
            if (spec)
            {
                special_event_properties.Add(property);
            }
            else
            {
                event_properties.Add(property);
            }
        }
    }
}
