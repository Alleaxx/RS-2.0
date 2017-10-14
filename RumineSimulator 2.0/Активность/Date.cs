using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Date
    {
        static public DateTime current_date { get; private set; }
        static public DateTime current_date_prev { get; private set; }
        static public DateTime found_date { get; private set; }
        static public int speed;

        public static void InitDate(DateTime Found_date, DateTime Curr_Date)
        {
            current_date = Curr_Date;
            current_date_prev = current_date;
            found_date = Found_date;
        }
        public static void TimeGo()
        {
            current_date_prev = new DateTime();
            current_date_prev = current_date;
            current_date = current_date.AddMinutes(1);

        }
        public static void SetTimeGoSpeed(int Speed)
        {
            speed = Speed;
        }

        public static bool LastDay()
        {
            if (current_date.Day == 31 && (current_date.Month == 1 || current_date.Month == 3 || current_date.Month == 5 || current_date.Month == 7 || current_date.Month == 8 || current_date.Month == 10 || current_date.Month == 12))
                return true;
            else if (current_date.Day == 30 && (current_date.Month == 4 || current_date.Month == 6 || current_date.Month == 9 || current_date.Month == 11))
                return true;
            else if (current_date.Day == 28 && current_date.Month == 2)
                return true;

            return false;
        }

        public static string ReturnCurrDate()
        {
            return current_date.ToShortTimeString() + "   " + current_date.ToShortDateString();
        }
    }
}
