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
        public long id;
        public ImageSource ImageSource { get; private set; }
        public string Name { get; private set; }
        public string Tooltip { get; private set; }
        public string BigDescription { get; private set; }
        public EventType EventType { get; private set; }
        public DateTime date { get; private set; }
        public int Duration { get; set; }
        public DateTime date_end { get; private set; }
        public InterfaceView_Event InterfaceInfo;
        public bool Reasonable { get; private set; }

        public int next_month_mod { get; private set; }
        public int next_week_mod { get; private set; }
        public int next_day_mod { get; private set; }
        public int current_day_mod { get; private set; }
        public int Reaction { get; private set; }

        public Event_Creator Creator { get; private set; }
        public Dictionary<User, string> participants = new Dictionary<User, string>();
        public List<Event> connected_events = new List<Event>();

        public Event(string name,EventType type)
        {
            id = Events_List.id;
            Events_List.id++;
            Name = name;
            EventType = type;
            date = Date.current_date;
            Events_List.AllEvents.Add(this);

        }
        public virtual void EventAdd1_BasicInfo(Event_Creator creator, string tooltip,bool reasonable = true)
        {
            Creator = creator;
            Tooltip = tooltip;
            Reasonable = reasonable;
        }
        public virtual void EventAdd2_Description(string description)
        {
            BigDescription = description;
        }
        public virtual void EventAdd3_Mods(int currDay_mod,int day_mod,int week_mod,int month_mod,int reaction = 1)
        {
            current_day_mod = currDay_mod;
            next_day_mod = day_mod;
            next_week_mod = week_mod;
            next_month_mod = month_mod;
            Reaction = reaction;
        }
        public virtual void EventAdd4_Participants(User user,string role)
        {
            participants.Add(user, role);
        }
        public virtual void EventAdd5_ImageSource(ImageSource source)
        {
            ImageSource = source;
        }
        public virtual void EventAdd6_Dates(int duration)
        {
            Duration = duration;
            date_end = date;
            date_end.AddMinutes(duration);
        }
        public virtual void EventAddEnd_InterfaceInfo()
        {
            InterfaceInfo = new InterfaceView_Event(this);
        }
    }

    enum EventType
    {
        message,comment,news,reputation,ban,fail,dayEnd
    }
}
