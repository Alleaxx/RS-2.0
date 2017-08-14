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
        public List<GuiString> connectedEntities_props = new List<GuiString>();

        //Создание информации события
        public IntView_Event(Event eve) : base()
        {
            //Базовая информация
            classic_string = new GuiString(eve.Name,"",true);
            classic_string.SetGUIName(GUITypes.simEvent, (Int32)eve.id);

            switch (eve.Importance)
            {
                case EventImportance.slight:
                    classic_string.SetColor("", "#FF575757");
                    break;
                case EventImportance.medium:
                    classic_string.SetColor("", "#FF2552E8");
                    break;
                case EventImportance.important:
                    classic_string.SetColor("", "#FF472581");
                    break;
                case EventImportance.historical:
                    classic_string.SetColor("", "#FFE88F25");
                    break;
            }

   
            Add_BasicEventProperty(classic_string);
            Add_BasicEventProperty(new GuiString("Дата: ", $"{eve.date.ToLongDateString()} {eve.date.ToShortTimeString()}"));
            if(eve.Duration != 0)
            {
                Add_BasicEventProperty(new GuiString("Длительность: ", eve.Duration.ToString()));
                Add_BasicEventProperty(new GuiString("Конец: ", $"{eve.date_end.ToLongDateString()} {eve.date_end.ToShortTimeString()}"));
            }
            basicEvent_props.AddRange(eve.eventSpec_properties);
            Add_BasicEventProperty(new GuiString("Прочие параметры","",false,StringProfile.Header));
            if (eve.Creator != null)
                Add_BasicEventProperty(new GuiString("Создатель: ", eve.Creator.Text));
            Add_BasicEventProperty(new GuiString("Тип: ", eve.EventType.ToString()));
            Add_BasicEventProperty(new GuiString($"Важность: ", $"{eve.Importance}({eve.daysToDelete})"));
            Add_BasicEventProperty(new GuiString("Параметр активности: ", eve.current_valMinute_mod.ToString()));
            if(eve.dayMod != 0)
                Add_BasicEventProperty(new GuiString("Мод. дня: ", eve.dayMod.ToString()));
            if (eve.weekMod != 0)
                Add_BasicEventProperty(new GuiString("Мод. недели: ", eve.weekMod.ToString()));
            if (eve.monthMod != 0)
                Add_BasicEventProperty(new GuiString("Мод. месяца: ", eve.monthMod.ToString()));


            //Участники
            Add_SpecEventConnected(new GuiString("Cписок участников: ", $"({eve.participants.Count})", false, StringProfile.Header));
            for (int i = 0; i < eve.participants.Count; i++)
            {
                GuiString user_reaction = new GuiString(eve.participants.ElementAt(i).Key.nick, "", false, StringProfile.Usual);
                user_reaction.SetColor("", eve.participants.ElementAt(i).Key.group.ColorHTML);
                Add_SpecEventConnected(user_reaction);
                Add_SpecEventConnected(new GuiString(eve.participants.ElementAt(i).Value, ""));
            }

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
            Add_Property(basicEvent_props, property);
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
