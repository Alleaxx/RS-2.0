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
        private DateTime ending;


        private int durance;
        public int Durance { get { return durance; } }

        private int id;
        public int Id { get { return id; } }

        private string name;
        public string Name { get { return name; } }


        public Action(string Name,int Durance)
        {
            durance = Durance;
            id = ActionControl.id_total + 1;
            name = Name;
        }

        //Начало события
        public void Begin()
        {
            beginning = Date.current_date;
            ending = beginning.AddMinutes(durance);
        }
        //Процесс события
        public bool Procees()
        {
            if (Date.current_date > ending)
                return true;
            else
                return false;
        }


        public IntView GetGui()
        {

            IntView view = new IntView();
            view.classic_string = new GuiString(name, id.ToString(), true, StringProfile.Usual);
            view.classic_string.SetGUIName(GUITypes.action, id);


            view.Add_Property(new GuiString("id", id.ToString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Начало",begin.ToString(),true,StringProfile.Usual));
            view.Add_Property(new GuiString("Длительность", durance.ToString(), true, StringProfile.Usual));
            view.Add_Property(new GuiString("Конец", ending.ToString(), true, StringProfile.Usual));
            return view;
        }
    }
}
