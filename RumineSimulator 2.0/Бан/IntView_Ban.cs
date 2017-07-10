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
            classic_string = new GuiString("Забанен? ", $"{ban.Banned}");
            classic_string.SetGUIName(GUITypes.ban, ban.id);

            Add_BanProperty(new GuiString("Свойства бана пользователя",$"{ban.Owner}",false,StringProfile.Header));
            Add_BanProperty(classic_string);
            Add_BanProperty(new GuiString("ID бана", $"{ban.id}", false));
            Add_BanProperty(new GuiString("Сумма предупреждений", $"{ban.Warn_sum}", true));
            Add_BanProperty(new GuiString("Бан админцентром? ", $"{ban.admin_center}", true));
            Add_BanProperty(new GuiString("Дата начала бана:  ", $"{ban.ban_start}", true));
            Add_BanProperty(new GuiString("Дата окончания бана: ", $"{ban.ban_end}", true));

        }
        public void Add_BanProperty(GuiString info)
        {
            Add_Property(ban_props, info);
        }
    }
}
