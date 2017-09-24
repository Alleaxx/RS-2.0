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
        public string Name { get; set; }
        public string sel_description { get; private set; }
        public List<string> descriptions = new List<string>();
        public EventType EventType { get; private set; }
        public EventType EventGlobalType { get; set; }

        public DateTime date { get; set; }
        public int Duration { get; set; }
        public DateTime date_end { get; private set; }
        //Информация для подробного просмотра
        public IntView_Event InterfaceInfo
        {
            get { return new IntView_Event(this); }
        }
        //Строка для представления в списках
        public GuiString InterfaceInfoClassicString { get; set; }
        public bool Reasonable { get; private set; }

        public float monthMod { get; private set; }
        public float weekMod { get; private set; }
        public float dayMod { get; private set; }
        public int current_valMinute_mod { get; private set; }

        //Важность события - зависит кол-во дней, которое оно не будет удаляться
        private EventImportance importance;
        public EventImportance Importance
        {
            get
            {
                return importance;
            }
            set
            {
                importance = value;
                switch (importance)
                {
                    case EventImportance.usual:
                        daysToDelete = 2;
                        break;
                    case EventImportance.unusual:
                        daysToDelete = 30;
                        break;
                    case EventImportance.rare:
                        daysToDelete = 180;
                        break;
                    case EventImportance.epic:
                        daysToDelete = 730;
                        break;
                    case EventImportance.historical:
                        daysToDelete = 2500;
                        break;
                }
            }
        }

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
            Importance = EventImportance.rare;
        }

        public virtual void EventAdd1_BasicInfo(Event_Creator creator)
        {
            Creator = creator;
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
        //Добавление полного описания(бывшие объявления)
        public void EventAdd7_Description(string adv)
        {
            descriptions.Add(adv);
        }
        public void EventAdd8_Color(string back = "",string fore = "")
        {

        }
        public void EventEnd_DescrChoose()
        {
            sel_description = AdvertisControl.GetAdvertisEvent(this);
            InterfaceInfoClassicString = InterfaceInfo.classic_string;
            InterfaceInfoClassicString.text_name.Text = $"{date.ToShortTimeString()}: {Name}";
            InterfaceInfoClassicString.SetGUIName(GUITypes.simEvent, (int)id);
        }

        public virtual void EventAction()
        {
            EventsControl.AllEvents.Add(this);
        }

        public virtual void DayPass()
        {
            daysToDelete--;
        }
    }

    enum EventType
    {
        usual, small, historic,
        message, comment, news, reputation, ban, fail, dayEnd,
        bigDiskussion, adminCome, userLeave,userCome,
        historicWiki,historicPaneAttack,HistorticFakeDay
    }
    enum EventImportance
    {
        usual,unusual,rare,epic,historical
    }
}
