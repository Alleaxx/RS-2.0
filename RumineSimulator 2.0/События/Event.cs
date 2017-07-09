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
        public EventType EventGlobalType { get; set; }
        public DateTime date { get; set; }
        public int Duration { get; set; }
        public DateTime date_end { get; private set; }
        public IntView_Event InterfaceInfo
        {
            get { return new IntView_Event(this); }
        }
        public bool Reasonable { get; private set; }

        public int next_month_mod { get; private set; }
        public int next_week_mod { get; private set; }
        public int next_day_mod { get; private set; }
        public int current_day_mod { get; private set; }
        public int Reaction { get; private set; }
        public int Days_Delete { get; set; }

        public Event_Creator Creator { get; private set; }
        public Dictionary<User, string> participants = new Dictionary<User, string>();
        public List<Event> connected_events = new List<Event>();
        public List<GuiString> props = new List<GuiString>();

        public Event(string name,EventType type)
        {
            id = Events_List.id + 1;
            Events_List.id++;
            Name = name;
            EventType = type;
            EventGlobalType = EventType.usual;
            date = Date.current_date;
            Days_Delete = 365;

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

        public virtual void EventAction()
        {
            Events_List.AllEvents.Add(this);
        }

        public virtual void DayPass()
        {
            Days_Delete--;
            List<GuiString> temp_spec = InterfaceInfo.specialEvent_props;
            List<GuiString> temp_con = InterfaceInfo.connectedEntities_props;
            InterfaceInfo.specialEvent_props = temp_spec;
            InterfaceInfo.connectedEntities_props = temp_con;
        }
    }

    enum EventType
    {
        usual, small, historic,
        message, comment, news, reputation, ban, fail, dayEnd,
        historicWiki,historicPaneAttack,HistorticFakeDay
    }
}
