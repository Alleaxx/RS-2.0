using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class GlobalParams
    {    
        //Условия получения определенного отношение(нестрогое сравнение)
        public const int friend_condition = 5;
        public const int comrade_condition = 2;
        public const int unfriends_condition = -1;
        public const int enemy_condition = -5;
        public const int rival_condition = 5;

        public static int warning_durings = 7;
    }
}
