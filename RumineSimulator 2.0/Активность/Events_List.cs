﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Events_List
    {
        static public long id = 0;
        static public List<Event> AllEvents = new List<Event>();
        static public List<SmallEvent> SmallEvents = new List<SmallEvent>();
        static public List<Event> BasicEvents = new List<Event>();
        static Random random = new Random();
        static public List<Interface_String> Events_properties = new List<Interface_String>();

        public static List<Interface_String> GetInterfaceInfo()
        {
            Events_properties.Add(new Interface_String("Количество всех событий",AllEvents.Count.ToString(),false));
            return Events_properties;
        }

        public static Event DayEnd()
        {
            Event Event = new Event("Окончание дня", EventType.dayEnd);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Rumine, "Румайн"), "Прошёл еще один день на румине",false);
            Event.EventAdd3_Mods(random.Next(0), 0, 0, 0);
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_EventProperty(new Interface_String("Прошедший день", Date.current_date_prev.ToShortDateString(), false, StringProfile.Header));
            Event.InterfaceInfo.Add_EventProperty(new Interface_String("Кол-во событий: ", Activity.today_events.Count.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new Interface_String("Кол-во сообщений: ", Activity.curr_day_messages.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new Interface_String("Кол-во комментариев: ", Activity.curr_day_comments.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new Interface_String("Кол-во новостей: ", Activity.curr_day_news.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new Interface_String("Кол-во измененений репутации: ", Activity.curr_day_repChanges.ToString(), true));
            Event.InterfaceInfo.Add_EventProperty(new Interface_String("Кол-во попыток забанить: ", Activity.curr_day_bans.ToString(), true));
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
                if (AllEvents[i].Days_Delete == 0)
                {
                    AllEvents.RemoveAt(i);
                }
                else
                {
                    i++;
                }

            }
            for (int i = 0; i < SmallEvents.Count;)
            {
                if (SmallEvents[i].Days_Delete == 0)
                {
                    SmallEvents.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
