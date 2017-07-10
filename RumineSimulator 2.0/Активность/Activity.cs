using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Activity
    {
        //Чистая активность на которую действуют модификаторы
        private static float current_vaActivity_Clear;
        public static float Current_valActivity_Clear
        {
            get { return current_vaActivity_Clear; }
            set
            {
                if (current_vaActivity_Clear >= 25 && current_vaActivity_Clear < 50)
                    current_vaActivity_Clear = value - 0.5F;
                else if (current_vaActivity_Clear >= 50 && current_vaActivity_Clear < 75)
                    current_vaActivity_Clear = value - 0.75F;
                else if (current_vaActivity_Clear >= 75 && current_vaActivity_Clear < 100)
                    current_vaActivity_Clear = value - 1.25F;
                else if (current_vaActivity_Clear > 100)
                    current_vaActivity_Clear = value - 2;
                else
                    current_vaActivity_Clear = value;

            }
        }

        //Настоящий параметр активности после применения модификаторов
        public static float current_valActivity_Real
        {
            get
            {
                return (current_vaActivity_Clear + day_modActivity + week_modActivity + month_modActivity) * hour_modActivity;
            }
        }


        //Часовой модификатор активности, оперирует десятыми
        public static float hour_modActivity
        {
            get; private set;
        }
        //Дневной модификатор активности, оперирует десятыми и сотыми
        public static float day_modActivity { get; private set; }
        //Недельный модификатор активности, оперирует сотыми
        public static float week_modActivity { get; private set; }
        //Месячный модификатор активности, оперирует сотыми
        public static float month_modActivity { get; private set; }

        public static int curr_day_messages { get; set; }
        public static int curr_day_comments { get; set; }
        public static int curr_day_news { get; set; }
        public static int curr_day_repChanges { get; set; }
        public static int curr_day_bans { get; set; }
        public static List<Event> today_events = new List<Event>();

        public static IntView_Activity InterfaceInfo
        {
            get
            {
                return new IntView_Activity();
            }
        }

        public static Event last_Event { get; private set; }
        public static Event last_SmallEvent { get; private set; }
        public static Event last_HistoricEvent { get; private set; }
        public static Event Last_Event
        {
            get
            {
                return last_Event;
            }
            private set
            {
                last_Event = value;
                if (last_Event.EventGlobalType == EventType.small)
                    last_SmallEvent = last_Event;
                if (last_Event.EventGlobalType == EventType.historic)
                    last_HistoricEvent = last_Event;
            }
        }

        private static Random random = new Random();

        public static void Activity_Init()
        {
            Current_valActivity_Clear = 50;
            hour_modActivity = 0.5F;
            day_modActivity = 1;
            week_modActivity = 1;
            month_modActivity = 1;
        }

        #region Определение прошедшего времени и операции с ним. Генерация модификаторов событий
        //Определение прошедшего времени
        public static void Time_Pass()
        {
            //Прошла минута
            if (Date.current_date.Hour == Date.current_date_prev.Hour)
                Minute_Pass();
            //Прошел час
            else if (Date.current_date.Hour != Date.current_date_prev.Hour && Date.current_date.Day == Date.current_date_prev.Day)
            {
                Minute_Pass();
                Hour_Pass();
            }
            //Прошёл день
            else if (Date.current_date.Day != Date.current_date_prev.Day)
            {
                Minute_Pass();
                Hour_Pass();
                Day_Pass();
            }
            //Прошел месяц
            else if (Date.current_date.Month != Date.current_date_prev.Month)
            {
                Minute_Pass();
                Hour_Pass();
                Day_Pass();
                Month_Pass();
            }
        }
        //Проверка минутных событий
        public static void Minute_Pass()
        {
            CheckEvents(false, false, false);
        }

        //Проверка часовых событий и модификатор дневной активности
        public static void Hour_Pass()
        {
            #region Активность в зависимости от времени дня
            switch (Date.current_date.Hour)
            {
                case 0:
                    hour_modActivity = 0.15F;
                    break;
                case 1:
                    hour_modActivity = 0.1F;
                    break;
                case 2:
                    hour_modActivity = 0.05F;
                    break;
                case 3:
                    hour_modActivity = 0.05F;
                    break;
                case 4:
                    hour_modActivity = 0.05F;
                    break;
                case 5:
                    hour_modActivity = 0.05F;
                    break;
                case 6:
                    hour_modActivity = 0.1F;
                    break;
                case 7:
                    hour_modActivity = 0.1F;
                    break;
                case 8:
                    hour_modActivity = 0.2F;
                    break;
                case 9:
                    hour_modActivity = 0.2F;
                    break;
                case 10:
                    hour_modActivity = 0.3F;
                    break;
                case 11:
                    hour_modActivity = 0.3F;
                    break;
                case 12:
                    hour_modActivity = 0.4F;
                    break;
                case 13:
                    hour_modActivity = 0.5F;
                    break;
                case 14:
                    hour_modActivity = 0.5F;
                    break;
                case 15:
                    hour_modActivity = 0.6F;
                    break;
                case 16:
                    hour_modActivity = 0.6F;
                    break;
                case 17:
                    hour_modActivity = 0.7F;
                    break;
                case 18:
                    hour_modActivity = 0.8F;
                    break;
                case 19:
                    hour_modActivity = 0.9F;
                    break;
                case 20:
                    hour_modActivity = 0.75F;
                    break;
                case 21:
                    hour_modActivity = 0.65F;
                    break;
                case 22:
                    hour_modActivity = 0.5F;
                    break;
                case 23:
                    hour_modActivity = 0.3F;
                    break;
            }
            CheckEvents(false, false, false);
            #endregion
        }
        public static void Day_Pass()
        {
            day_modActivity = 1;
            EventsControl.CheckEventsDelete();
            CheckEvents(true, false, false);
            DayStatisticsUpdate();
        }
        public static void DayStatisticsUpdate()
        {
            curr_day_messages = 0;
            curr_day_bans = 0;
            curr_day_comments = 0;
            curr_day_repChanges = 0;
            curr_day_news = 0;
            today_events.Clear();
        }
        public static void Week_Pass()
        {
            week_modActivity = 1;
        }
        public static void Month_Pass()
        {
            month_modActivity = 1;
        }

        public static void CheckEvents(bool newday, bool newweek, bool newmonth)
        {
            List<Event> new_events = new List<Event>();


            //Сообщение
            if (AdvRnd.PersentChanseBool((int)current_valActivity_Real))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.message));
            }
            //Комментарий
            if (AdvRnd.PersentChanseBool((int)current_valActivity_Real /10))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.comment));
            }
            //Новость
            if (AdvRnd.PersentChanseBool((int)(current_valActivity_Real/10),600))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.news));
            }
            //Изменение репутации
            if (AdvRnd.PersentChanseBool((int)(current_valActivity_Real / 10), 500))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.reputation));
            }
            if (AdvRnd.PersentChanseBool((int)(current_valActivity_Real / 10), 1000))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.ban));
            }


            //Действия с новыми событиями
            foreach (Event new_event in new_events)
            {
                EventsControl.AllEvents.Add(new_event);
                Last_Event = new_event;
                LastEvent_ModsModifier();
            }

            HistoricEvent h_event = HistoricEvents_List.EventCheck();
            if (h_event != null)
            {
                Last_Event = h_event;
                EventsControl.AllEvents.Add(Last_Event);
                LastEvent_ModsModifier();

            }

            //Новый день
            if (newday)
            {
                Last_Event = EventsControl.DayEnd();
                LastEvent_ModsModifier();
            }
        }
        public static void LastEvent_ModsModifier()
        {
            Current_valActivity_Clear += Last_Event.current_valMinute_mod;
            day_modActivity += Last_Event.dayMod;
            month_modActivity += Last_Event.monthMod;
            week_modActivity += Last_Event.weekMod;
            today_events.Add(Last_Event);
        }
        #endregion
    }
}
