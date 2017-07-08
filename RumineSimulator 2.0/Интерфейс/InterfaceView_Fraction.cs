using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_Fraction : InterfaceView
    {
        public List<Interface_String> fraction_properties = new List<Interface_String>();
        //Создание ГУИ на основе фракции
        public InterfaceView_Fraction(Fraction fraction) : base()
        {
            string_info = new Interface_String(fraction.name, "", true);
            if (!fraction.active)
                string_info.AddColor("#FFDDDDDD", "#FF000000");
            if(fraction.leader != null)
                Add_FractionProperty(new Interface_String("Лидер: ", fraction.leader.nick, false));
            Add_FractionProperty(new Interface_String("Активность: ", fraction.active.ToString(), false));
            Add_FractionProperty(new Interface_String("Членов: ", fraction.members.Count.ToString(), false));
            Add_FractionProperty(new Interface_String("Форумное влияние: ", fraction.Influence.ToString(), false));
            Add_FractionProperty(new Interface_String("Идеология: ",$"", false, StringProfile.Header));
            foreach (Traits trait in fraction.ideology_traits)
            {
                Add_FractionProperty(new Interface_String(trait.ToString(), "", false,StringProfile.Quote));
            }
            Add_FractionProperty(new Interface_String("Вражеская идеология: ", $"{(fraction.enemy_traits.Count)}",false, StringProfile.Header));
            foreach (Traits trait in fraction.enemy_traits)
            {
                Add_FractionProperty(new Interface_String(trait.ToString(), "", false, StringProfile.Quote));
            }
            Add_FractionProperty(new Interface_String("Вражеские фракции: ", $"({fraction.enemy_fractions.Count})", false, StringProfile.Header));
            foreach (Fraction enemy_fraction in fraction.enemy_fractions)
            {
                Add_FractionProperty(new Interface_String(enemy_fraction.name.ToString(), "", false, StringProfile.Quote));
            }
            Add_FractionProperty(new Interface_String("Члены фракции: ", $"", false, StringProfile.Header));
            foreach (User user in fraction.members)
            {
                Add_FractionProperty(new Interface_String(user.nick, "", false, StringProfile.Quote));
            }
        }

        //Добавление строк в списки свойств фракции
        public void Add_FractionProperty(Interface_String info)
        {
            Add_Property(fraction_properties, info);
        }
    }
}
