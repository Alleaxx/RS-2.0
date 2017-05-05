using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class GlobalParams
    {
        public static bool Testing { get; set; }
        public static bool GodMode { get; set; }

        //Условия получения определенного отношение(нестрогое сравнение)
        public const int friend_condition = 4;
        public const int comrade_condition = 2;
        public const int unfriends_condition = -1;
        public const int enemy_condition = -4;
        public const int rival_condition = 5;

        //Количество объектов
        public static int relation_obj { get; set; }
    }
}
