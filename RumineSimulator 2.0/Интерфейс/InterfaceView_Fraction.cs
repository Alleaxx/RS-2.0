using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_Fraction : InterfaceView
    {
        public List<Interface_String> basic_properties = new List<Interface_String>();
        public InterfaceView_Fraction(Fraction fraction) : base()
        {
            string_info = new Interface_String(fraction.name, "", true);
            if (!fraction.active)
                string_info.AddColor("#FF707070", "#FFD3D3D3");
            if(fraction.leader != null)
                basic_properties.Add(new Interface_String("Лидер: ", fraction.leader.nick, false));
            basic_properties.Add(new Interface_String("Активность: ", fraction.active.ToString(), false));
            basic_properties.Add(new Interface_String("Членов: ", fraction.members.Count.ToString(), false));
            basic_properties.Add(new Interface_String("Форумное влияние: ", fraction.Influence.ToString(), false));
            basic_properties.Add(new Interface_String("Идеология: ",$"", false, true));
            foreach (Traits trait in fraction.ideology_traits)
            {
                basic_properties.Add(new Interface_String(trait.ToString(), "", false));
            }
            basic_properties.Add(new Interface_String("Вражеская идеология: ", $"{(fraction.enemy_traits.Count)}",false,true));
            foreach (Traits trait in fraction.enemy_traits)
            {
                basic_properties.Add(new Interface_String(trait.ToString(), "", false));
            }
            basic_properties.Add(new Interface_String("Вражеские фракции: ", $"({fraction.enemy_fractions.Count})", false, true));
            foreach (Fraction enemy_fraction in fraction.enemy_fractions)
            {
                basic_properties.Add(new Interface_String(enemy_fraction.name.ToString(), "", false));
            }
            basic_properties.Add(new Interface_String("Члены фракции: ", $"", false, true));
            foreach (User user in fraction.members)
            {
                basic_properties.Add(new Interface_String(user.nick, "", false));
            }



        }
    }
}
