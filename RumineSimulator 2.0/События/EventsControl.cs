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

        public static Event EventSearch(long id)
        {
            for (int i = 0; i < AllEvents.Count; i++)
            {
                if (AllEvents[i].id == id)
                    return AllEvents[i];
            }
            return AllEvents[0];
        }
        //Выдача списка событий, связанных с юзером(любая связь подойдет, даже просто участие)
        public static List<Event> EventSearch(User author)
        {
            List<Event> auth_events = new List<Event>();
            for (int i = 0; i < AllEvents.Count; i++)
            {
                if ((AllEvents[i].Creator.Text == author.nick || AllEvents[i].participants.ContainsKey(author))
                    && AllEvents[i].Importance != EventImportance.slight )
                    auth_events.Add(AllEvents[i]);
            }
            auth_events.Reverse();
            return auth_events;
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
