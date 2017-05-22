using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class SmallEvents_List
    {
        static public List<SmallEvent> SmallEvents = new List<SmallEvent>();
        static Random random = new Random();
        public static SmallEvent MessageWrite()
        {
            User user = UserList.Users[random.Next(UserList.Users.Count)];
            SmallEvent Event = new SmallEvent($"Сообщение от {user}",EventType.message);
            return Event;
        }
    }
}
