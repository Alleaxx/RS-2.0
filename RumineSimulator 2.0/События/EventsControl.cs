using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class EventsControl
    {
        static public long id = 0;
        static public List<Event> AllEvents = new List<Event>();
        static public List<Event> BasicEvents = new List<Event>();
        static Random random = new Random();
        static public List<GuiString> Events_properties = new List<GuiString>();

        public static List<GuiString> GetInterfaceInfo()
        {
            Events_properties.Add(new GuiString("Количество всех событий",AllEvents.Count.ToString(),false));
            return Events_properties;
        }








        public static Event DayEnd()
        {
            Event Event = new Event("Окончание дня", EventType.dayEnd);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Rumine, "Румайн"));
            Event.EventAdd3_Mods(random.Next(0), 0, 0, 0);
            Event.InterfaceInfo.Add_EventProperty(new GuiString("Прошедший день", Date.current_date_prev.ToShortDateString(), false, StringProfile.Header));
            Event.InterfaceInfo.Add_EventProperty(new GuiString("Кол-во событий: ", Activity.today_events.Count.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new GuiString("Кол-во сообщений: ", Activity.curr_day_messages.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new GuiString("Кол-во комментариев: ", Activity.curr_day_comments.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new GuiString("Кол-во новостей: ", Activity.curr_day_news.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new GuiString("Кол-во измененений репутации: ", Activity.curr_day_repChanges.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new GuiString("Кол-во попыток забанить: ", Activity.curr_day_bans.ToString(), true));
            BasicEvents.Add(Event);
            return Event;
        }
        public static Event EventSearch(long id)
        {
            for (int i = 0; i < AllEvents.Count; i++)
            {
                if (AllEvents[i].id == id)
                    return AllEvents[i];
            }
            return AllEvents[0];
        }

        public static void CheckEventsDelete()
        {
            for (int i = 0; i < AllEvents.Count; )
            {
                AllEvents[i].DayPass();
                if (AllEvents[i].daysToDelete == 0)
                {
                    AllEvents.RemoveAt(i);
                }
                else
                {
                    i++;
                }

            }
        }
    }
}
