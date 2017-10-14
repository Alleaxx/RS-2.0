using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Statistics : IntView
    {
        public List<GuiString> statistics = new List<GuiString>();

        public IntView_Statistics()
        {
            Add_StatProperty((new GuiString("Активность", $"", false, StringProfile.Header)));
            Add_StatProperty((new GuiString("Всего написано сообщений: ", $"{Statistic.total_messages}", false)));
            Add_StatProperty((new GuiString("Пользователи", $"", false, StringProfile.Header)));
            Add_StatProperty((new GuiString("Средняя адекватность: ", $"{Statistic.aver_adeq}", false)));
            Add_StatProperty((new GuiString("Средняя раковитость: ", $"{Statistic.aver_rakness}", false)));
            Add_StatProperty((new GuiString("Средняя консервативность: ", $"{Statistic.aver_conservative}", false)));
            Add_StatProperty((new GuiString("Средняя толерантность: ", $"{Statistic.aver_tolerance}", false)));
            Add_StatProperty((new GuiString("Средняя наука: ", $"{Statistic.aver_sciense}", false)));
            Add_StatProperty((new GuiString("Средняя креативность: ", $"{Statistic.aver_creativity}", false)));
            Add_StatProperty((new GuiString("Средняя гуманитарность: ", $"{Statistic.aver_humanist}", false)));
            Add_StatProperty((new GuiString("Средняя историчность: ", $"{Statistic.aver_historic}", false)));
            Add_StatProperty((new GuiString("Средний шанс уйти: ", $"{Statistic.aver_leaveChanse}", false)));

            Add_StatProperty((new GuiString("Техническое", $"", false, StringProfile.Header)));
            Add_StatProperty((new GuiString("Обращений к отношениям: ", $"{Statistic.relation_obj}", false)));
            Add_StatProperty((new GuiString("Создано GUI-строк: ", $"{Statistic.guiString_obj}", false)));


            Add_StatProperty((new GuiString("Глобальные параметры", $"", false, StringProfile.Header)));
            Add_StatProperty((new GuiString("Условие дружбы: ", $"{GlobalParams.friend_condition}", false)));
            Add_StatProperty((new GuiString("Условие товарищечества: ", $"{GlobalParams.comrade_condition}", false)));
            Add_StatProperty((new GuiString("Условие неприязни: ", $"{GlobalParams.unfriends_condition}", false)));
            Add_StatProperty((new GuiString("Условие вражды: ", $"{GlobalParams.enemy_condition}", false)));
            Add_StatProperty((new GuiString("Условие соперничества: ", $"{GlobalParams.rival_condition}", false)));

            Add_StatProperty((new GuiString("Длительность бана предупреждениями: ", $"{GlobalParams.warning_durings}", false)));

        }
        public void Add_StatProperty(GuiString info)
        {
            Add_Property(statistics, info);
        }
    }
}
