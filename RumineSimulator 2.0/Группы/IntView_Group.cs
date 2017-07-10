using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Group : IntView
    {
        public List<GuiString> group_props = new List<GuiString>();
        public IntView_Group(Group group)
        {
            classic_string = new GuiString(group.Name, "", true);
            classic_string.SetColor("", group.ColorHTML);
            classic_string.SetGUIName(GUITypes.group, group.id);

            Add_GroupProperty(classic_string);
            Add_GroupProperty(new GuiString("ID группы: ", $"{group.id}"));
            Add_GroupProperty(new GuiString("Тип группы: ", $"{group.type}"));
            Add_GroupProperty(new GuiString("Уважение: ", $"{group.Respect}"));
            Add_GroupProperty(new GuiString("Редкость: ", $"{group.Rareness}"));
            //Условия группы
            Add_GroupProperty(new GuiString("Условия", $"", false, StringProfile.Header));
            Add_GroupProperty(new GuiString("Особые условия получения: ", $"{group.condition.special_condition}"));
            Add_GroupProperty(new GuiString("Олдфажность: ", $"{group.condition.month_oldness}"));
            Add_GroupProperty(new GuiString("Карма: ", $"{group.condition.Karma}"));
            Add_GroupProperty(new GuiString("Репутация: ", $"{group.condition.Reputation}"));
            Add_GroupProperty(new GuiString("Новости: ", $"{group.condition.News}"));
            Add_GroupProperty(new GuiString("Качество новостей: ", $"{group.condition.News_quality}"));
            //Возможности группы
            Add_GroupProperty(new GuiString("Возможности", $"",false,StringProfile.Header));
            Add_GroupProperty(new GuiString("Модераторские права: ", $"{group.Mod}"));
            Add_GroupProperty(new GuiString("Возможности журналиста: ", $"{group.Journ}"));
            Add_GroupProperty(new GuiString("Возможности модератора бездны: ", $"{group.Mod_bezdn}"));
            Add_GroupProperty(new GuiString("Админцентр: ", $"{group.aC}"));
            Add_GroupProperty(new GuiString("Админ: ", $"{group.admin}"));
        }
        public void Add_GroupProperty(GuiString info)
        {
            Add_Property(group_props, info);
        }
    }
}
