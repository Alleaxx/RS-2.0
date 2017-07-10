using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Event : IntView
    {
        public List<GuiString> basicEvent_props = new List<GuiString>();
        public List<GuiString> specialEvent_props = new List<GuiString>();
        public List<GuiString> connectedEntities_props = new List<GuiString>();

        //Создание информации события
        public IntView_Event(Event eve) : base()
        {
            //Базовая информация
            classic_string = new GuiString(eve.Name,"",true);
            classic_string.SetGUIName(GUITypes.simEvent, (Int32)eve.id);
            Add_BasicEventProperty(new GuiString("Основные свойства: ", "", false, StringProfile.Header));
            Add_BasicEventProperty(new GuiString("Дата: ", $"{eve.date.ToLongDateString()} {eve.date.ToShortTimeString()}"));
            Add_BasicEventProperty(new GuiString("Длительность: ", eve.Duration.ToString()));
            Add_BasicEventProperty(new GuiString("Конец: ", $"{eve.date_end.ToLongDateString()} {eve.date_end.ToShortTimeString()}"));
            Add_BasicEventProperty(new GuiString("Тип: ", eve.EventType.ToString()));
            if(eve.Creator == null)
                Add_BasicEventProperty(new GuiString("Неизвестно", ""));
            else
                Add_BasicEventProperty(new GuiString("Создатель: ", eve.Creator.Text));
            Add_BasicEventProperty(new GuiString("Мод. дня: ", eve.next_day_mod.ToString()));
            Add_BasicEventProperty(new GuiString("Мод. недели: ", eve.next_week_mod.ToString()));
            Add_BasicEventProperty(new GuiString("Мод. месяца: ", eve.next_month_mod.ToString()));
            Add_BasicEventProperty(new GuiString($"Реактивность({eve.Reasonable}): ", eve.Reaction.ToString()));
            Add_BasicEventProperty(new GuiString($"Дней до удаления: ", eve.Days_Delete.ToString()));

            //Участники
            Add_SpecEventConnected(new GuiString("Cписок участников: ", $"({eve.participants.Count})", false, StringProfile.Header));
            for (int i = 0; i < eve.participants.Count; i++)
            {
                GuiString user_reaction = new GuiString(eve.participants.ElementAt(i).Key.nick, "", false, StringProfile.Usual);
                user_reaction.SetColor("", eve.participants.ElementAt(i).Key.group.ColorHTML);
                Add_SpecEventConnected(user_reaction);
                Add_SpecEventConnected(new GuiString(eve.participants.ElementAt(i).Value, ""));
            }

            specialEvent_props = eve.props;

            //Запись связанных событий
            if(eve.connected_events.Count != 0)
            {
                Add_SpecEventConnected(new GuiString("Связанные события", $"({eve.connected_events.Count})", false, StringProfile.Header));
                for (int i = 0; i < eve.connected_events.Count; i++)
                {
                    Add_SpecEventConnected(new GuiString(eve.connected_events[i].Name, $"({eve.connected_events[i].id})"));
                }
            }
            else
            {
                Add_SpecEventConnected(new GuiString("Связанных событий нет", "", false, StringProfile.Header));
            }

            //Добавление исторических
            if (eve.EventGlobalType == EventType.historic)
            {
                HistoricEvent eve_history = (HistoricEvent)eve;
                for (int i = 0; i < eve_history.more_properties.Count; i++)
                {
                    Add_EventProperty(new GuiString(eve_history.more_properties.ElementAt(i).Key, eve_history.more_properties.ElementAt(i).Value,
                        true));
                }
            }

        }



        //Добавление строк в списки свойств события
        public void Add_EventProperty(GuiString property)
        {
            Add_Property(specialEvent_props,property);
        }
        public void Add_SpecEventConnected(GuiString property)
        {
            Add_Property(connectedEntities_props, property);
        }
        public void Add_BasicEventProperty(GuiString property)
        {
            Add_Property(basicEvent_props,property);
        }
    }
}
