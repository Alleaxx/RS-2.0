using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Activity : IntView
    {
        public List<GuiString> activity_props = new List<GuiString>();

        public IntView_Activity()
        {
            Add_ActivityProperty(new GuiString("Чистый параметр активности: ", $"{Activity.Current_valActivity_Clear}", false));
            Add_ActivityProperty(new GuiString("Часовой мод: ", $"{Activity.hour_modActivity}", false));
            Add_ActivityProperty(new GuiString("Дневной мод: ", $"{Activity.day_modActivity}", false));
            Add_ActivityProperty(new GuiString("Недельный мод: ", $"{Activity.week_modActivity}", false));
            Add_ActivityProperty(new GuiString("Месячный мод: ", $"{Activity.month_modActivity}", false));
            Add_ActivityProperty(new GuiString("Реальный параметр активности: ", Activity.current_valActivity_Real.ToString(), false));
            //if (Activity.Last_Event != null)
            //{

            //    Add_ActivityProperty(new GuiString("Последнее событие", Activity.Last_Event.Name, false));
            //}
            //else
            //{
            //    Add_ActivityProperty(new GuiString("Событий пока нет", "", false));
            //}
        }


        //Добавление строк в списки свойств активности
        public void Add_ActivityProperty(GuiString info)
        {
            Add_Property(activity_props, info);
        }
    }
}
