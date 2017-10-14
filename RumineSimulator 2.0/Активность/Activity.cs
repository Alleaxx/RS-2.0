using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Activity
    {
        public static event EventHandler<ActivityEventArgs> ActivityInfoChanged;
        public static event EventHandler<ActivityEventArgs> NewEventAdded;

        //Чистая активность на которую действуют модификаторы
        private static float current_vaActivity_Clear;
        public static float Current_valActivity_Clear
        {
            get { return current_vaActivity_Clear; }
            set
            {
                if (current_vaActivity_Clear > 100)
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


        //Статистика активности
        #region Статистика активности
        public static string floodTopic_name = "Форумный чат в 3.14";
        public static int total_pages = 0;
        private static int mess_on_last_page = 0;
        //Рассчет кол-ва сообщений, а так же страниц во флудилке
        private static int hour_messages;
        public static int Hour_messages
        {
            get
            {
                return hour_messages;
            }
            set
            {
                hour_messages = value;
                mess_on_last_page++;
                if (mess_on_last_page > 20)
                {
                    mess_on_last_page = 0;
                    total_pages++;
                }
            }
        }

        public static int day_messages { get; set; }
        public static int week_messages { get; set; }
        public static int month_messages { get; set; }
        public static int year_messages { get; set; }

        public static int day_comments { get; set; }
        public static int week_comments { get; set; }
        public static int month_comments { get; set; }
        public static int year_comments { get; set; }

        public static int day_news { get; set; }
        public static int wekk_news { get; set; }
        public static int month_news { get; set; }
        public static int year_news { get; set; }

        public static int day_repChanges { get; set; }
        public static int week_repChanges { get; set; }
        public static int month_repChanges { get; set; }
        public static int year_repChanges { get; set; }

        public static int day_bans { get; set; }
        public static int week_bans { get; set; }
        public static int month_bans { get; set; }
        public static int year_bans { get; set; }


        #endregion


        public static List<Event> day_events = new List<Event>();

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

        private static List<EventType> planned_todayEvents = new List<EventType>();

        public static void Activity_Init()
        {
            Current_valActivity_Clear = 50;
            hour_modActivity = 0.5F;
            day_modActivity = 1;
            week_modActivity = 1;
            month_modActivity = 1;
        }

        #region Определение прошедшего времени и операции с ним. Генерация модификаторов событий
        //Определение прошедшего времени, событие на изменение интерфейса
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
            ActivityInfoChanged(Current_valActivity_Clear, new ActivityEventArgs("Активность изменилась"));
        }
        //Проверка минутных событий
        public static void Minute_Pass()
        {
            //Постоянное убывание активности
            if (current_vaActivity_Clear >= 25 && current_vaActivity_Clear < 50)
                current_vaActivity_Clear -= 0.25F;
            else if (current_vaActivity_Clear >= 50 && current_vaActivity_Clear < 75)
                current_vaActivity_Clear -= 0.5F;
            else if (current_vaActivity_Clear >= 75 && current_vaActivity_Clear < 100)
                current_vaActivity_Clear -= 0.75F;

            CheckEvents(false, false, false);
            ActivityInfoChanged(Current_valActivity_Clear, new ActivityEventArgs("Убывание активности"));
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
                    hour_modActivity = 0.1F;
                    break;
                case 6:
                    hour_modActivity = 0.15F;
                    break;
                case 7:
                    hour_modActivity = 0.15F;
                    break;
                case 8:
                    hour_modActivity = 0.2F;
                    break;
                case 9:
                    hour_modActivity = 0.3F;
                    break;
                case 10:
                    hour_modActivity = 0.3F;
                    break;
                case 11:
                    hour_modActivity = 0.35F;
                    break;
                case 12:
                    hour_modActivity = 0.5F;
                    break;
                case 13:
                    hour_modActivity = 0.55F;
                    break;
                case 14:
                    hour_modActivity = 0.6F;
                    break;
                case 15:
                    hour_modActivity = 0.65F;
                    break;
                case 16:
                    hour_modActivity = 0.7F;
                    break;
                case 17:
                    hour_modActivity = 0.75F;
                    break;
                case 18:
                    hour_modActivity = 0.85F;
                    break;
                case 19:
                    hour_modActivity = 0.9F;
                    break;
                case 20:
                    hour_modActivity = 0.8F;
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
            day_messages += hour_messages;
            hour_messages = 0;

        }
        public static void Day_Pass()
        {
            day_modActivity = 1;
            EventsControl.CheckEventsDelete();
            CheckEvents(true, false, false);
            DayStatisticsUpdate();
        }
        //Обновление статистики за день
        public static void DayStatisticsUpdate()
        {
            week_messages += day_messages;
            day_messages = 0;
            week_bans += day_bans;
            day_bans = 0;
            week_comments += day_comments;
            day_comments = 0;
            week_repChanges += day_repChanges;
            day_repChanges = 0;
            wekk_news += day_news;
            day_news = 0;
            day_events.Clear();
        }
        public static void Week_Pass()
        {
            week_modActivity = 1;
        }
        public static void Month_Pass()
        {
            month_modActivity = 1;
        }


        //Событие на добавление нового события в интерфейс
        public static void CheckEvents(bool newday, bool newweek, bool newmonth)
        {
            List<Event> new_events = new List<Event>();


            //Сообщение
            if (AdvRnd.PrsChanse((int)current_valActivity_Real))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.message));
            }
            //Комментарий
            if (AdvRnd.PrsChanse((int)current_valActivity_Real / 10))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.comment));
            }
            //Новость
            if (AdvRnd.PrsChanse((int)(current_valActivity_Real / 10), 600))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.news));
            }
            //Изменение репутации
            if (AdvRnd.PrsChanse((int)(current_valActivity_Real / 10), 500))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.reputation));
            }
            if (AdvRnd.PrsChanse((int)(current_valActivity_Real / 10), 5000))
            {
                new_events.Add(EventStatChange_Preset.returnStatChangeEvent(EventType.ban));
            }

            //Проверка на планируемые события
            for (int i = 0; i < planned_todayEvents.Count; i++)
            {
                if (AdvRnd.PrsChanse((int)(hour_modActivity * 10), 1000))
                {
                    new_events.Add(EventUsualDay_Preset.returnUsualDayEvent(planned_todayEvents[i]));
                    planned_todayEvents.RemoveAt(i);
                    i--;
                }
            }

            //Новый день, планировка событий, которые должны произойти в течение дня
            if (newday)
            {
                new_events.Add(EventUsualDay_Preset.returnUsualDayEvent(EventType.dayEnd));


                //Заход админов на сайт
                if (AdvRnd.PrsChanse(25))
                {
                    planned_todayEvents.Add(EventType.adminCome);
                }
                //Крупная дискуссия
                if (AdvRnd.PrsChanse(55))
                {
                    planned_todayEvents.Add(EventType.bigDiskussion);
                }
                //Уход и приход пользователей
                if (AdvRnd.PrsChanse(1))
                {
                    planned_todayEvents.Add(EventType.userLeave);
                }
                if (AdvRnd.PrsChanse(1))
                {
                    planned_todayEvents.Add(EventType.userCome);
                }

            }

            //Действия с новыми событиями
            foreach (Event new_event in new_events)
            {
                EventsControl.AllEvents.Add(new_event);
                Last_Event = new_event;
                LastEvent_ModsModifier();
                NewEventAdded(new_event,new ActivityEventArgs("Новое событие"));
            }

            HistoricEvent h_event = HistoricEvents_List.EventCheck();
            if (h_event != null)
            {
                Last_Event = h_event;
                EventsControl.AllEvents.Add(Last_Event);
                LastEvent_ModsModifier();
            }


        }
        //Событие на изменение параметров активности в интерфейсе
        public static void LastEvent_ModsModifier()
        {
            Current_valActivity_Clear += Last_Event.current_valMinute_mod;
            day_modActivity += Last_Event.dayMod;
            month_modActivity += Last_Event.monthMod;
            week_modActivity += Last_Event.weekMod;
            day_events.Add(Last_Event);
        }
        #endregion
    }
}
