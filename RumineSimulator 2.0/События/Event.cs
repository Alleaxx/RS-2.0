using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    abstract class Event
    {
        //id и изображение
        private int id;
        public int ID { get { return id; } }

        private ImageSource imageSource;

        //Имя, описание
        public string Name { get; set; }
        public string description { get; set; }

        public DateTime date { get; set; }
        //Строка для представления в списках
        public bool Reasonable { get; private set; }

        public float monthMod { get; private set; }
        public float weekMod { get; private set; }
        public float dayMod { get; private set; }
        public int current_valMinute_mod { get; private set; }

        //Редкость события
        private EventImportance rareness;
        public EventImportance Rareness
        {
            get
            {
                return rareness;
            }
            set
            {
                rareness = value;
                switch (rareness)
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

        //Участники и создатель
        public Dictionary<User, string> participants = new Dictionary<User, string>();
        public User Creator { get; set; }

        public List<GuiString> eventSpec_properties = new List<GuiString>();

        public Event()
        {
            EventsControl.UsersRandomisation();
            id = EventsControl.id + 1;
            EventsControl.id++;
            Name = "Новое событие";
            date = Date.current_date;
            Rareness = EventImportance.rare;
        }

        //Модификаторы события
        public void AddMods(int currDay_mod,float day_mod,float week_mod,float month_mod)
        {
            current_valMinute_mod = currDay_mod;
            dayMod = day_mod;
            weekMod = week_mod;
            monthMod = month_mod;
        }
        //Участники события
        public void AddParticipant(User user,string role)
        {
            participants.Add(user, role);
        }

        public virtual void EventAction()
        {
            Creator = participants.ElementAt(0).Key;
        }
        public void DayPass()
        {
            daysToDelete--;
        }

        public IntView GetGui()
        {
            IntView view = new IntView();
            //Базовая информация
            view.classic_string = new GuiString(Name, "", true);
            view.classic_string.SetGUIName(GUITypes.simEvent, (Int32)id);

            switch (Rareness)
            {
                case EventImportance.usual:
                    view.classic_string.Item.IsHitTestVisible = false;
                    view.classic_string.SetColor("", "#FF575757");
                    break;
                case EventImportance.unusual:
                    view.classic_string.SetColor("", "#FF2EAA28");
                    break;
                case EventImportance.rare:
                    view.classic_string.SetColor("", "#FF2552E8");
                    break;
                case EventImportance.epic:
                    view.classic_string.SetColor("", "#FF472581");
                    break;
                case EventImportance.historical:
                    view.classic_string.SetColor("", "#FFE88F25");
                    break;
            }

            view.Add_Property(new GuiString("Дата: ", $"{date.ToLongDateString()} {date.ToShortTimeString()}"));
            view.all_properties.AddRange(eventSpec_properties);
            view.Add_Property(new GuiString("Прочие параметры", "", false, StringProfile.Header));
            if (Creator != null)
                view.Add_Property(new GuiString("Создатель: ", Creator.nick));
            view.Add_Property(new GuiString("Тип: ", ToString()));
            view.Add_Property(new GuiString($"Важность: ", $"{Rareness}({daysToDelete})"));
            view.Add_Property(new GuiString("Параметр активности: ", current_valMinute_mod.ToString()));
            if (dayMod != 0)
                view.Add_Property(new GuiString("Мод. дня: ", dayMod.ToString()));
            if (weekMod != 0)
                view.Add_Property(new GuiString("Мод. недели: ", weekMod.ToString()));
            if (monthMod != 0)
                view.Add_Property(new GuiString("Мод. месяца: ", monthMod.ToString()));


            //Участники
            view.Add_Property(new GuiString("Cписок участников: ", $"({participants.Count})", false, StringProfile.Header));
            for (int i = 0; i < participants.Count; i++)
            {
                GuiString user_reaction = new GuiString(participants.ElementAt(i).Key.nick, "", false, StringProfile.Usual);
                user_reaction.SetColor("", participants.ElementAt(i).Key.group.ColorHTML);
                view.Add_Property(user_reaction);
                view.Add_Property(new GuiString(participants.ElementAt(i).Value, ""));
            }


            return view;
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
