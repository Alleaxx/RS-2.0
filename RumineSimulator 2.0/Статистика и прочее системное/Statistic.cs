using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Statistic
    {
        //Пользователи
        static public int users_amount
        {
            get
            {
                return UsersControl.act_users.Count;
            }
        }
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
