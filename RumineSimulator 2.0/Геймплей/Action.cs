using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Action
    {
        //Событие завершения действия
        public event EventHandler<ActionEventArgs> ShowEnd;

        //Временные свойства
        private DateTime beginning;
        public bool begin { get; set; }
        public DateTime ending
        {
            get
            {
                return beginning.AddMinutes(durance);
            }
        }
        private int durance;
        public int Durance { get { return durance; } }
        private int remaining;
        public int Remaining { get { return remaining; } }
        public bool ended { get; set; }

        //id, имя, описание, результат
        private int id;
        public int Id { get { return id; } }
        private string name;
        public string Name { get { return name; } }
        private string description;
        public string Description { get { return description; } set { description = value; } }

        public string result { get; set; }

        //Выбранная игроком конфигурация действия
        ActionInfo Information = new ActionInfo();


        //Конструкторы
        public Action(string Name,int Durance)
        {
            durance = Durance;
            remaining = durance;
            id = ActionControl.id_total + 1;
            name = Name;
            description = "Действие без заранее заданного описания";
        }
        public Action() : this("", 10)
        {

        }
        public Action(ActionInfo information) : this("", 10)
        {
            Information = information;
        }

        //Начало события
        public void Begin()
        {
            begin = true;
            beginning = Date.current_date;
        }

        //Процесс события и его завершение
        public bool Procees()
        {
            remaining--;
            if (remaining == 0)
            {
                Ending();
                ShowEnd(this, new ActionEventArgs(this));
                return true;
            }
            else
                return false;
        }
        public virtual void Ending()
        {
            ended = true;
            result = $"{ending.ToShortTimeString()}: Эффекта от действия нет. Тем не менее оно завершилось и вы его больше не увидите";
        }

        //GUI
        public IntView GetGui()
        {

            IntView view = new IntView();
            view.classic_string = new GuiString(name, id.ToString(), true, StringProfile.Usual);
            view.classic_string.SetGUIName(GUITypes.action, id);
            //Главные параметры
            view.Add_Property(new GuiString(name, "", false, StringProfile.Header));
            view.Add_Property(new GuiString("id: ", id.ToString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Тип: ", this.ToString(), true, StringProfile.Usual));
            if(begin)
                view.Add_Property(new GuiString("Начало", beginning.ToShortTimeString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Длится в минутах: ", durance.ToString(), true, StringProfile.Usual));
            //Дополнительно
            view.Add_Property(new GuiString("Дополнительно","", false, StringProfile.Header));
            if (ended)
                view.Add_Property(new GuiString("Завершено","", true, StringProfile.Usual));

            return view;
        }
        public GuiProgressBar GetClassicString()
        {
            GuiProgressBar str = new GuiProgressBar($"{name} ({remaining})", remaining, 0, durance, StringProfile.Usual);
            str.SetSize(14, 14);
            str.SetGUIName(GUITypes.action,id);
            return str;
        }
    }
}
