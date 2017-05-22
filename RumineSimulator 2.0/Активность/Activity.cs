using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Activity
    {
        public static int Current_mod_activity { get; set; }
        public static int current_mod_activity_withoutTime
        {
            get { return Current_mod_activity; }
            set { Current_mod_activity = value; }
        }
        public static int NextDay_mod_activity { get; private set; }
        public static int NextWeek_mod_activity { get; private set; }
        public static int NextMonth_mod_activity { get; private set; }
        public static InterfaceView_Activity InterfaceInfo
        {
            get
            {
                InterfaceView_Activity Info = new InterfaceView_Activity();
                Info.Add_Property(new Interface_String("Мод. активности", Current_mod_activity.ToString(), false));
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
            Current_mod_activity = 5;
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
            else if (Date.current_date.Day != Date.current_date_prev.Day)
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
                    Current_mod_activity = Current_mod_activity / 50;
                    break;
                case 7:
                    Current_mod_activity = Current_mod_activity / 30;
                    break;
                case 8:
                    Current_mod_activity = Current_mod_activity / 5;
                    break;
                case 9:
                    Current_mod_activity = Current_mod_activity / 5;
                    break;
                case 10:
                    Current_mod_activity = Current_mod_activity / 5;
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
                    Current_mod_activity = Current_mod_activity / 2;
                    break;
                case 15:
                    Current_mod_activity = Current_mod_activity;
                    break;
                case 16:
                    Current_mod_activity = Current_mod_activity;
                    break;
                case 17:
                    Current_mod_activity = Current_mod_activity;
                    break;
                case 18:
                    Current_mod_activity = Current_mod_activity + 5;
                    break;
                case 19:
                    Current_mod_activity = Current_mod_activity + 5;
                    break;
                case 20:
                    Current_mod_activity = Current_mod_activity + 10;
                    break;
                case 21:
                    Current_mod_activity = Current_mod_activity + 5;
                    break;
                case 22:
                    Current_mod_activity = Current_mod_activity / 2; ;
                    break;
                case 23:
                    Current_mod_activity = Current_mod_activity / 5; ;
                    break;
            }

            #endregion
        }
        public static void Day_Pass()
        {
            Current_mod_activity = Current_mod_activity / 100 + NextDay_mod_activity;
            NextDay_mod_activity = 0;
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
            if (AdvRandom.PersentChanseBool(20 + (Current_mod_activity / 10)))
            {
                Last_Event = SmallEvents_List.MessageWrite();
                LastEvent_ModsModifier();
            }
        }
        public static void LastEvent_ModsModifier()
        {
            Current_mod_activity += Last_Event.current_day_mod;
            NextDay_mod_activity += Last_Event.next_day_mod;
            NextWeek_mod_activity += Last_Event.next_week_mod;
            NextMonth_mod_activity += Last_Event.next_month_mod;
        }
        #endregion
    }
}
