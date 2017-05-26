using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Activity
    {
        private static int current_mod_activity;
        public static int Current_mod_activity
        {
            get { return current_mod_activity; }
            set
            {
                if (current_mod_activity > 100)
                    current_mod_activity = value - 1;
                else if (current_mod_activity > 200)
                    current_mod_activity = value - 2;
                else
                    current_mod_activity = value;

            }
        }
        public static int CurrentDay_mod_activity { get; set; }
        public static int current_mod_activity_withoutTime
        {
            get { return Current_mod_activity; }
            set { Current_mod_activity = value; }
        }
        public static int NextDay_mod_activity { get; private set; }
        public static int NextWeek_mod_activity { get; private set; }
        public static int NextMonth_mod_activity { get; private set; }

        public static int curr_day_messages { get; set; }
        public static int curr_day_comments { get; set; }
        public static int curr_day_news { get; set; }
        public static int curr_day_repChanges { get; set; }
        public static int curr_day_bans { get; set; }
        public static List<Event> today_events = new List<Event>();
        public static InterfaceView_Activity InterfaceInfo
        {
            get
            {
                InterfaceView_Activity Info = new InterfaceView_Activity();
                Info.Add_Property(new Interface_String("Мод. активности", Current_mod_activity.ToString(), false));
                Info.Add_Property(new Interface_String("Мод. активности без времени", current_mod_activity_withoutTime.ToString(), false));
                Info.Add_Property(new Interface_String("Мод. на сегодня", CurrentDay_mod_activity.ToString(), false));
                Info.Add_Property(new Interface_String("Мод. дневной активности", NextDay_mod_activity.ToString(), false));
                Info.Add_Property(new Interface_String("Мод. недельной активности", NextWeek_mod_activity.ToString(), false));
                Info.Add_Property(new Interface_String("Мод. месячной активности", NextMonth_mod_activity.ToString(), false));
                try
                {
                    Info.Add_Property(new Interface_String("Последнее событие", Last_Event.Name, false));
                }
                catch
                {
                    Info.Add_Property(new Interface_String("Событий пока нет", "", false));
                }
                return Info;
            }
        }

        public static Event Last_Event { get; private set; }

        public static void Activity_Init()
        {
            Current_mod_activity = 20;
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
            current_mod_activity_withoutTime = Current_mod_activity;
            Current_mod_activity = Current_mod_activity + CurrentDay_mod_activity / 50;
            switch (Date.current_date.Hour)
            {
                case 0:
                    Current_mod_activity = Current_mod_activity / 20;
                    break;
                case 1:
                    Current_mod_activity = Current_mod_activity / 40;
                    break;
                case 2:
                    Current_mod_activity = Current_mod_activity / 60;
                    break;
                case 3:
                    Current_mod_activity = Current_mod_activity / 70;
                    break;
                case 4:
                    Current_mod_activity = Current_mod_activity / 80;
                    break;
                case 5:
                    Current_mod_activity = Current_mod_activity / 60;
                    break;
                case 6:
                    Current_mod_activity = Current_mod_activity / 50 + 5;
                    break;
                case 7:
                    Current_mod_activity = Current_mod_activity / 30 + 10;
                    break;
                case 8:
                    Current_mod_activity = Current_mod_activity / 5 + 10;
                    break;
                case 9:
                    Current_mod_activity = Current_mod_activity / 5 + 10;
                    break;
                case 10:
                    Current_mod_activity = Current_mod_activity / 4 + 5;
                    break;
                case 11:
                    Current_mod_activity = Current_mod_activity / 3;
                    break;
                case 12:
                    Current_mod_activity = Current_mod_activity / 2;
                    break;
                case 13:
                    Current_mod_activity = Current_mod_activity / 2;
                    break;
                case 14:
                    Current_mod_activity = Current_mod_activity / 3 * 2;
                    break;
                case 15:
                    Current_mod_activity = Current_mod_activity / 3 * 2;
                    break;
                case 16:
                    Current_mod_activity = Current_mod_activity / 3 * 2;
                    break;
                case 17:
                    Current_mod_activity = Current_mod_activity / 4 * 3;
                    break;
                case 18:
                    Current_mod_activity = Current_mod_activity;
                    break;
                case 19:
                    Current_mod_activity = Current_mod_activity;
                    break;
                case 20:
                    Current_mod_activity = Current_mod_activity / 4 * 3;
                    break;
                case 21:
                    Current_mod_activity = Current_mod_activity / 3 * 2 - 10;
                    break;
                case 22:
                    Current_mod_activity = Current_mod_activity / 3 - 5; ;
                    break;
                case 23:
                    Current_mod_activity = Current_mod_activity / 4; ;
                    break;
            }
            CheckEvents(false, false, false);
            #endregion
        }
        public static void Day_Pass()
        {
            CheckEvents(true, false, false);
            DayStatisticsUpdate();
            CurrentDay_mod_activity = NextDay_mod_activity;
            NextDay_mod_activity = 0;
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
            Current_mod_activity = Current_mod_activity / 100 + NextDay_mod_activity / 10;
            NextWeek_mod_activity = 0;
        }
        public static void Month_Pass()
        {

        }

        public static void CheckEvents(bool newday, bool newweek, bool newmonth)
        {
            //Сообщение
            if (AdvRandom.PersentChanseBool(1 + (Current_mod_activity)))
            {
                Last_Event = SmallEvents_List.MessageWrite();
                LastEvent_ModsModifier();
                //Реакция на события
                CheckLastEvent_Reaction();
            }
            //Комментарий
            if (AdvRandom.PersentChanseBool(1 + (Current_mod_activity / 20) ))
            {
                Last_Event = SmallEvents_List.CommentWrite();
                LastEvent_ModsModifier();
                //Реакция на события
                CheckLastEvent_Reaction();
            }
            //Новость
            if (AdvRandom.PersentChanseBool(current_mod_activity,10000))
            {
                Last_Event = SmallEvents_List.NewsWrite();
                LastEvent_ModsModifier();
                //Реакция на события
                CheckLastEvent_Reaction();
            }
            //Рандомная репутация
            if (AdvRandom.PersentChanseBool(current_mod_activity, 2000))
            {
                Last_Event = SmallEvents_List.ReputationChange_Random();
                LastEvent_ModsModifier();
                //Реакция на события
                CheckLastEvent_Reaction();
            }
            //Рандомный бан
            if (AdvRandom.PersentChanseBool(Current_mod_activity, 10000))
            {
                Last_Event = SmallEvents_List.Ban_Random();
                LastEvent_ModsModifier();
                //Реакция на события
                CheckLastEvent_Reaction();
            }
            
            //Новый день
            if (newday)
            {
                Last_Event = Events_List.DayEnd();
                LastEvent_ModsModifier();
                CheckLastEvent_Reaction();
            }
        }
        public static void CheckLastEvent_Reaction()
        {
            Event reaction = CheckEvent_Reaction(Last_Event);
            if (reaction != null && Last_Event.Reasonable)
            {
                Last_Event = reaction;
                LastEvent_ModsModifier();
                CheckLastEvent_Reaction();
            }
        }
        public static Event CheckEvent_Reaction(Event reason)
        {
            if (AdvRandom.PersentChanseBool(Current_mod_activity + Last_Event.Reaction,10000))
            {
                return SmallEvents_List.Ban_Reason(reason);
            }
            else if(AdvRandom.PersentChanseBool(current_mod_activity + Last_Event.Reaction, 2000))
            {
                return SmallEvents_List.ReputationChange_Reason(reason);
            }
            else
            {
                return null;
            }
        }
        public static void LastEvent_ModsModifier()
        {
            Current_mod_activity += Last_Event.current_day_mod;
            NextDay_mod_activity += Last_Event.next_day_mod;
            NextWeek_mod_activity += Last_Event.next_week_mod;
            NextMonth_mod_activity += Last_Event.next_month_mod;
            today_events.Add(Last_Event);
        }
        #endregion
    }
}
