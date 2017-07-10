using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Trait : IntView
    {
        public List<GuiString> trait_props = new List<GuiString>(); 

        public IntView_Trait(Trait trait)
        {
            //Строковое представление
            classic_string = new GuiString(trait.name, "", true);
            classic_string.SetToolTip(trait.Tooltip);
            classic_string.SetGUIName(GUITypes.trait, trait.id_num);
            classic_string.SetColor(trait.background_brush, trait.foreground_brush);
            //Свойства
            Add_TraitProperty(classic_string);
            Add_TraitProperty(new GuiString("ID трейта: ", trait.id_num.ToString(), false));
            Add_TraitProperty(new GuiString("ID трейта №2: ", trait.type.ToString(), false));
            Add_TraitProperty(new GuiString("Тип трейта: ", trait.global_type.ToString(), false));
            Add_TraitProperty(new GuiString("Шанс появления: ", trait.chanse.ToString() + "%", false));
            //Условия трейта
            Add_TraitProperty(new GuiString("Условия", $"({trait.conditions.Count})", false, StringProfile.Header));
            if (trait.conditions.Count != 0)
            {
                for (int i = 0; i < trait.conditions.Count; i++)
                {
                    Add_TraitProperty(new GuiString(trait.conditions.ElementAt(i).Key.ToString(), trait.conditions.ElementAt(i).Value.ToString(), false));
                }
            }
            else
            {
                Add_TraitProperty(new GuiString("Условий нет", trait.Tooltip, false));
            }
            //Заблокированные трейты
            Add_TraitProperty(new GuiString("Заблокированные трейты", $"({trait.blocked_types.Count})", false, StringProfile.Header));
            if (trait.blocked_types.Count != 0)
            {
                foreach (TraitsType tr in trait.blocked_types)
                {
                    Add_TraitProperty(new GuiString(tr.ToString(),"",false));
                }
            }
            else
            {
                Add_TraitProperty(new GuiString("Заблокированных трейтов нет", "", false));
            }

        }

        public void Add_TraitProperty(GuiString info)
        {
            Add_Property(trait_props, info);
        }
    }
}
