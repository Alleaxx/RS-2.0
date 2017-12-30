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
