using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_Event : InterfaceView
    {
        public List<Interface_String> basic_event_properties = new List<Interface_String>();
        public List<Interface_String> special_event_properties = new List<Interface_String>();
        public List<Interface_String> connectedEntities_properties = new List<Interface_String>();

        //Создание информации события
        public InterfaceView_Event(Event eve) : base()
        {
            //Базовая информация
            string_info = new Interface_String(eve.Name,"",true);
            Add_BasicEventProperty(new Interface_String("Основные свойства: ", "", false, StringProfile.Header));
            Add_BasicEventProperty(new Interface_String("Дата: ", $"{eve.date.ToLongDateString()} {eve.date.ToShortTimeString()}"));
            Add_BasicEventProperty(new Interface_String("Длительность: ", eve.Duration.ToString()));
            Add_BasicEventProperty(new Interface_String("Конец: ", $"{eve.date_end.ToLongDateString()} {eve.date_end.ToShortTimeString()}"));
            Add_BasicEventProperty(new Interface_String("Тип: ", eve.EventType.ToString()));
            if(eve.Creator == null)
                Add_BasicEventProperty(new Interface_String("Неизвестно", ""));
            else
                Add_BasicEventProperty(new Interface_String("Создатель: ", eve.Creator.Text));
            Add_BasicEventProperty(new Interface_String("Мод. дня: ", eve.next_day_mod.ToString()));
            Add_BasicEventProperty(new Interface_String("Мод. недели: ", eve.next_week_mod.ToString()));
            Add_BasicEventProperty(new Interface_String("Мод. месяца: ", eve.next_month_mod.ToString()));
            Add_BasicEventProperty(new Interface_String($"Реактивность({eve.Reasonable}): ", eve.Reaction.ToString()));
            Add_BasicEventProperty(new Interface_String($"Дней до удаления: ", eve.Days_Delete.ToString()));

            //Участники
            Add_SpecEventConnected(new Interface_String("Cписок участников: ", $"({eve.participants.Count})", false, StringProfile.Header));
            for (int i = 0; i < eve.participants.Count; i++)
            {
                Interface_String user_reaction = new Interface_String(eve.participants.ElementAt(i).Key.nick, "", false, StringProfile.Usual);
                user_reaction.AddColor("", eve.participants.ElementAt(i).Key.group.ColorHTML);
                Add_SpecEventConnected(user_reaction);
                Add_SpecEventConnected(new Interface_String(eve.participants.ElementAt(i).Value, ""));
            }

            //Запись связанных событий
            if(eve.connected_events.Count != 0)
            {
                Add_SpecEventConnected(new Interface_String("Связанные события", $"({eve.connected_events.Count})", false, StringProfile.Header));
                for (int i = 0; i < eve.connected_events.Count; i++)
                {
                    Add_SpecEventConnected(new Interface_String(eve.connected_events[i].Name, $"({eve.connected_events[i].id})"));
                }
            }
            else
            {
                Add_SpecEventConnected(new Interface_String("Связанных событий нет", "", false, StringProfile.Header));
            }

            //Добавление исторических
            if (eve.EventGlobalType == EventType.historic)
            {
                HistoricEvent eve_history = (HistoricEvent)eve;
                for (int i = 0; i < eve_history.more_properties.Count; i++)
                {
                    Add_EventProperty(new Interface_String(eve_history.more_properties.ElementAt(i).Key, eve_history.more_properties.ElementAt(i).Value,
                        true));
                }
            }

        }



        //Добавление строк в списки свойств события
        public void Add_EventProperty(Interface_String property)
        {
            Add_Property(special_event_properties,property);
        }
        public void Add_SpecEventConnected(Interface_String property)
        {
            Add_Property(connectedEntities_properties, property);
        }
        public void Add_BasicEventProperty(Interface_String property)
        {
            Add_Property(basic_event_properties,property);
        }
    }
}
