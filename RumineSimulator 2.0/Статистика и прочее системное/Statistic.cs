using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Statistic
    {
        public static int total_messages;
        //Пользователи
        static public int users_amount
        {
            get
            {
                return UsersControl.act_users.Count;
            }
        }

        static public float aver_rakness { get; set; }
        static public float aver_adeq { get; set; }
        static public float aver_tolerance { get; set; }
        static public float aver_sciense { get; set; }
        static public float aver_creativity { get; set; }
        static public float aver_conservative { get; set; }
        static public float aver_humanist { get; set; }
        static public float aver_historic { get; set; }
        static public float aver_leaveChanse { get; set; }
        //Количество объектов
        public static int relation_obj { get; set; }
        public static int guiString_obj { get; set; }

        public static IntView_Statistics InterfaceInfo
        {
            get
            {
                return new IntView_Statistics();
            }
        }


    }
}
