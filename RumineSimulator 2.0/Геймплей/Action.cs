using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Action
    {
        private DateTime beginning;
        public bool begin { get; set; }
        public DateTime ending;


        private int durance;
        public int Durance { get { return durance; } }

        private int remaining;
        public int Remaining { get { return remaining; } }

        private int id;
        public int Id { get { return id; } }

        private string name;
        public string Name { get { return name; } }
        private string description;
        public string Description { get { return description; } }

        public ActionDo ActDo;


        public Action(string Name,int Durance,ActionType type)
        {
            durance = Durance;
            remaining = durance;
            id = ActionControl.id_total + 1;
            name = Name;
            description = "Действие без заранее заданного описания";
            ActDo = new ActionDo(this,type);
        }

        public void SetDescr(string descr)
        {
            description = descr;
        }

        //Начало события
        public void Begin()
        {
            begin = true;
            beginning = Date.current_date;
            ending = beginning.AddMinutes(durance);
        }
        //Процесс события
        public bool Procees()
        {
            remaining--;
            if (Date.current_date >= ending)
            {
                ActDo.Ending(this);
                return true;
            }
            else
                return false;
        }

        private void Ending()
        {

        }


        public IntView GetGui()
        {

            IntView view = new IntView();
            view.classic_string = new GuiString(name, id.ToString(), true, StringProfile.Usual);
            view.classic_string.SetGUIName(GUITypes.action, id);


            view.Add_Property(new GuiString("id", id.ToString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Тип", ActDo.type.ToString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Начало",beginning.ToString(),true,StringProfile.Usual));
            view.Add_Property(new GuiString("Длительность", durance.ToString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Подробности действия","", false, StringProfile.Header));
            view.Add_Property(new GuiString("Завершенность действия", ActDo.succes.ToString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Тип длительности", ActDo.time.ToString(), true, StringProfile.Usual));

            return view;
        }
        public GuiProgressBar GetClassicString()
        {
            GuiProgressBar str = new GuiProgressBar($"{name} ({remaining})", remaining, 0, durance, StringProfile.Usual);
            str.SetSize(14, 14);
            str.SetGUIName(GUITypes.action, id);
            return str;
        }
    }
}
