using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Events_List
    {
        static public long id = 0;
        static public List<Event> AllEvents = new List<Event>();
        static Random random = new Random();
        static public List<Interface_String> Events_properties = new List<Interface_String>();

        public static List<Interface_String> GetInterfaceInfo()
        {
            Events_properties.Add(new Interface_String("Количество всех событий",AllEvents.Count.ToString(),false));
            return Events_properties;
        }

    }
}
