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
            Add_ActivityProperty(new GuiString($"{Activity.floodTopic_name}", $"", false, StringProfile.Header));
            Add_ActivityProperty(new GuiString("Чистый параметр активности: ", $"{Activity.Current_valActivity_Clear}", false));
            Add_ActivityProperty(new GuiString("Часовой мод: ", $"{Activity.hour_modActivity}", false));
            Add_ActivityProperty(new GuiString("Дневной мод: ", $"{Activity.day_modActivity}", false));
            Add_ActivityProperty(new GuiString("Недельный мод: ", $"{Activity.week_modActivity}", false));
            Add_ActivityProperty(new GuiString("Месячный мод: ", $"{Activity.month_modActivity}", false));
            Add_ActivityProperty(new GuiString("Реальный параметр активности: ", Activity.current_valActivity_Real.ToString(), false));
            Add_ActivityProperty(new GuiString("Статистика событий", $"", false,StringProfile.Header));
            Add_ActivityProperty(new GuiString("Кол-во сообщений за час: ", $"{Activity.Hour_messages}", false));
            Add_ActivityProperty(new GuiString("Страниц в ФЧ: ", $"{Activity.total_pages}", false));


        }


        //Добавление строк в списки свойств активности
        public void Add_ActivityProperty(GuiString info)
        {
            Add_Property(activity_props, info);
        }
    }
}
