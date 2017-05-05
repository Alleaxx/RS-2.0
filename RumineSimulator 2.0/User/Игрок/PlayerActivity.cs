using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class PlayerActivity
    {
        static Random random = new Random();
        public static PlayerActivities activity = new PlayerActivities();
        private static int sleep_time { get; set; }
        private static int time_to_sleep { get; set; }
        private static int active_time { get; set; }
        private static int time_to_active { get; set; }

        static bool leto = true;
        public static void ActivityInit()
        {
            activity = PlayerActivities.freetime;
            active_time = random.Next(720, 960);
            time_to_active = active_time;
            sleep_time = random.Next(240, 720);
        }
        public static void UpdateActivity()
        {
            if(activity == PlayerActivities.sleep)
            {
                time_to_sleep--;
                if(time_to_sleep <= 0)
                {
                    active_time = random.Next(840, 1020);
                    time_to_active = active_time;
                    if (leto)
                    {
                        activity = PlayerActivities.freetime;
                    }
                }
            }
            else if(activity == PlayerActivities.freetime)
            {
                time_to_active--;
                if(time_to_active <= 0)
                {
                    sleep_time = sleep_time = random.Next(300, 720);
                    time_to_sleep = sleep_time;
                    activity = PlayerActivities.sleep;
                }
            }
        }

        public static string ReturnActivityText()
        {
            switch (activity)
            {
                case PlayerActivities.sleep:
                    return "Сон";
                case PlayerActivities.freetime:
                    return "Бодрствование";
                default:
                    return "Хз";
            }
        }
    }
    enum PlayerActivities
    {
        sleep,
        freetime,
        profession
    }
}
