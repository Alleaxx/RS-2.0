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
            classic_string = new GuiString(trait.short_name, "", true);
            classic_string.AddToolTip(trait.full_description);
            classic_string.SetGUIName(GUITypes.trait, trait.id_num);
            classic_string.AddColor(trait.background_brush, trait.foreground_brush);
            //Свойства
            Add_TraitProperty(classic_string);
            Add_TraitProperty(new GuiString("ID трейта: ", trait.id_num.ToString(), false));
            Add_TraitProperty(new GuiString("ID трейта №2: ", trait.id_trait.ToString(), false));
            Add_TraitProperty(new GuiString("Тип трейта: ", trait.type.ToString(), false));
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
                Add_TraitProperty(new GuiString("Условий нет", trait.full_description, false));
            }
            //Заблокированные трейты
            Add_TraitProperty(new GuiString("Заблокированные трейты", $"({trait.blocked_traits.Count})", false, StringProfile.Header));
            if (trait.blocked_traits.Count != 0)
            {
                foreach (Trait tr in trait.blocked_traits)
                {
                    Add_TraitProperty(new GuiString(tr.short_name,"",false));
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
