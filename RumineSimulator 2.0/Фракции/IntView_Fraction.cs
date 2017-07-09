using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Fraction : IntView
    {
        public List<GuiString> fraction_properties = new List<GuiString>();
        //Создание ГУИ на основе фракции
        public IntView_Fraction(Fraction fraction) : base()
        {
            classic_string = new GuiString(fraction.name, "", true);
            classic_string.SetGUIName(GUITypes.fraction, fraction.id);
            Add_FractionProperty(classic_string);
            if (!fraction.active)
                classic_string.AddColor("#FFDDDDDD", "#FF000000");
            if(fraction.leader != null)
                Add_FractionProperty(new GuiString("Лидер: ", fraction.leader.nick, false));
            Add_FractionProperty(new GuiString("Активность: ", fraction.active.ToString(), false));
            Add_FractionProperty(new GuiString("Форумное влияние: ", fraction.Influence.ToString(), false));
            Add_FractionProperty(new GuiString("Идеология: ",$"", false, StringProfile.Header));
            foreach (Traits trait in fraction.ideology_traits)
            {
                Add_FractionProperty(new GuiString(trait.ToString(), "", false,StringProfile.Quote));
            }
            Add_FractionProperty(new GuiString("Вражеская идеология: ", $"{(fraction.enemy_traits.Count)}",false, StringProfile.Header));
            foreach (Traits trait in fraction.enemy_traits)
            {
                Add_FractionProperty(new GuiString(trait.ToString(), "", false, StringProfile.Quote));
            }
            Add_FractionProperty(new GuiString("Вражеские фракции: ", $"({fraction.enemy_fractions.Count})", false, StringProfile.Header));
            foreach (Fraction enemy_fraction in fraction.enemy_fractions)
            {
                Add_FractionProperty(new GuiString(enemy_fraction.name.ToString(), "", false, StringProfile.Quote));
            }
            Add_FractionProperty(new GuiString("Члены фракции: ", $"({fraction.members.Count.ToString()})", false, StringProfile.Header));
            foreach (User user in fraction.members)
            {
                Add_FractionProperty(new GuiString(user.nick, $"({user.forum_influence})", false, StringProfile.Quote));
                fraction_properties.Last().AddColor("", user.group.ColorHTML);
                fraction_properties.Last().SetSize(12, 11);
            }
        }

        //Добавление строк в списки свойств фракции
        public void Add_FractionProperty(GuiString info)
        {
            Add_Property(fraction_properties, info);
        }
    }
}
