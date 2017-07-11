using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    class Event : IAdvertisable
    {
        public long id;
        public ImageSource ImageSource { get; private set; }
        public string Name { get; private set; }
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

        public float monthMod { get; private set; }
        public float weekMod { get; private set; }
        public float dayMod { get; private set; }
        public int current_valMinute_mod { get; private set; }

        public int daysToDelete { get; set; }

        public Event_Creator Creator { get; private set; }
        public Dictionary<User, string> participants = new Dictionary<User, string>();
        public List<Event> connected_events = new List<Event>();


        public List<GuiString> eventSpec_properties = new List<GuiString>();

        public Event(string name,EventType type)
        {
            id = EventsControl.id + 1;
            EventsControl.id++;
            Name = name;
            EventType = type;
            EventGlobalType = EventType.usual;
            date = Date.current_date;
            daysToDelete = 365;

        }

        public virtual void EventAdd1_BasicInfo(Event_Creator creator)
        {
            Creator = creator;
        }
        public virtual void EventAdd2_Description(string description)
        {
            BigDescription = description;
        }
        public virtual void EventAdd3_Mods(int currDay_mod,float day_mod,float week_mod,float month_mod)
        {
            current_valMinute_mod = currDay_mod;
            dayMod = day_mod;
            weekMod = week_mod;
            monthMod = month_mod;
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
            EventsControl.AllEvents.Add(this);
        }

        public virtual void DayPass()
        {
            daysToDelete--;
        }


        #region IAdvertisable
        public List<string> Advertisments = new List<string>();
        public void AddAdvertisment(string adv)
        {
            Advertisments.Add(adv);
        }
        #endregion
    }

    enum EventType
    {
        usual, small, historic,
        message, comment, news, reputation, ban, fail, dayEnd,
        historicWiki,historicPaneAttack,HistorticFakeDay
    }
}
