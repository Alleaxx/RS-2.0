using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class EventDescription
    {
        public static string ReturnDescription(Event eve)
        {
            string descr = "";
            switch (eve.EventType)
            {
                case EventType.message:
                    break;
                default:
                    descr = "Это описание события. И его тут почему-то нет";
                    break;
            }
            return descr;
        }
    }
}
