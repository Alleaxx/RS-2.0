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
            IntView_Activity Info = new IntView_Activity();
            Info.Add_ActivityProperty(new GuiString("Мод. активности", Activity.Current_mod_activity.ToString(), false));
            Info.Add_ActivityProperty(new GuiString("Мод. активности без времени", Activity.current_mod_activity_withoutTime.ToString(), false));
            Info.Add_ActivityProperty(new GuiString("Мод. на сегодня", Activity.CurrentDay_mod_activity.ToString(), false));
            Info.Add_ActivityProperty(new GuiString("Мод. дневной активности", Activity.NextDay_mod_activity.ToString(), false));
            Info.Add_ActivityProperty(new GuiString("Мод. недельной активности", Activity.NextWeek_mod_activity.ToString(), false));
            Info.Add_ActivityProperty(new GuiString("Мод. месячной активности", Activity.NextMonth_mod_activity.ToString(), false));
            if (Activity.Last_Event != null)
            {

                Info.Add_ActivityProperty(new GuiString("Последнее событие", Activity.Last_Event.Name, false));
            }
            else
            {
                Info.Add_ActivityProperty(new GuiString("Событий пока нет", "", false));
            }
        }


        //Добавление строк в списки свойств активности
        public void Add_ActivityProperty(GuiString info)
        {
            Add_Property(activity_props, info);
        }
    }
}
