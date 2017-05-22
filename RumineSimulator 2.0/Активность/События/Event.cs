using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    class Event
    {
        public ImageSource ImageSource { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EventType EventType { get; private set; }
        public DateTime date { get; private set; }

        public int next_month_mod { get; private set; }
        public int next_week_mod { get; private set; }
        public int next_day_mod { get; private set; }
        public int current_day_mod { get; private set; }

        public Event_Creator Creator { get; private set; }
        public Dictionary<User, string> participants = new Dictionary<User, string>();

        public Event(string name,EventType type)
        {
            Name = name;
            EventType = type;
            date = Date.current_date;
            BasicEvents_List.AllEvents.Add(this);
        }
        public virtual void EventAdd1_BasicInfo(Event_Creator creator,string description)
        {
            Creator = creator;
            if (Creator.Type == CreatorType.User)
                participants.Add(UserList.UserSearch(Creator.Text),"Создавал и ваял");
            Description = description;
        }
        public virtual void EventAdd2_Mods(int currDay_mod,int day_mod,int week_mod,int month_mod)
        {
            current_day_mod = currDay_mod;
            next_day_mod = day_mod;
            next_week_mod = week_mod;
            next_month_mod = month_mod;
        }
        public virtual void EventAdd3_Participants(User user,string role)
        {
            participants.Add(user, role);
        }
        public virtual void EventAdd4_ImageSource(ImageSource source)
        {
            ImageSource = source;
        }
    }

    enum EventType
    {
        message
    }
}
