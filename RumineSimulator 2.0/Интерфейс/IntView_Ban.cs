using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Ban : IntView
    {
        public List<GuiString> ban_props = new List<GuiString>();

        public IntView_Ban(Ban ban)
        {
            classic_string = new GuiString("Бан", ban.Banned.ToString());
            classic_string.SetGUIName(GUITypes.ban, ban.id);
        }
    }
}
