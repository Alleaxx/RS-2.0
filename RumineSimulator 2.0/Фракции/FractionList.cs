using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class FractionList
    {
        public static Fraction NeutralFraction;
        public static List<Fraction> AllFractions = new List<Fraction>();
        public static int id_total = 0;
        public static void FractionsInit()
        {
            List<Traits> ideol = new List<Traits>();
            List<Traits> enemy = new List<Traits>();
            NeutralFraction = new Fraction("Нейтралы", ideol, enemy, FractionAggression.peacefull,false, true);

            ideol = new List<Traits>() {Traits.commie};
            enemy = new List<Traits>() { Traits.fashik};
            AllFractions.Add(new Fraction("Верующие коммунисты",ideol,enemy,FractionAggression.light,false));

            ideol = new List<Traits>() { };
            enemy = new List<Traits>() { Traits.rak };
            AllFractions.Add(new Fraction("Общество раковых гурманов", ideol, enemy, FractionAggression.aggressive, false));

            ideol = new List<Traits>() { Traits.rak };
            enemy = new List<Traits>() { Traits.inteligent };
            AllFractions.Add(new Fraction("1-е Румынские Народные Освободители", ideol, enemy, FractionAggression.aggressive, false));

            ideol = new List<Traits>() { Traits.nazi };
            enemy = new List<Traits>() { Traits.newfag};
            AllFractions.Add(new Fraction("Форумные нацисты", ideol, enemy, FractionAggression.aggressive, false));

            ideol = new List<Traits>() { Traits.fashik };
            enemy = new List<Traits>() { Traits.commie };
            AllFractions.Add(new Fraction("Верующие фашисты", ideol, enemy, FractionAggression.ambigious,false));

            ideol = new List<Traits>() { Traits.ded,Traits.natural };
            enemy = new List<Traits>() { Traits.nevecherya };
            AllFractions.Add(new Fraction("Совет дедов-натуралов", ideol, enemy, FractionAggression.normal,true));

            ideol = new List<Traits>() { Traits.programmer, Traits.arter,Traits.writer };
            enemy = new List<Traits>() { Traits.rak };
            AllFractions.Add(new Fraction("MicroShit", ideol, enemy, FractionAggression.light,false));

            ideol = new List<Traits>() { Traits.pane};
            enemy = new List<Traits>() {};
            AllFractions.Add(new Fraction("Броняши", ideol, enemy, FractionAggression.ambigious, false));

            ideol = new List<Traits>() {};
            enemy = new List<Traits>() { Traits.pane };
            AllFractions.Add(new Fraction("Хейтеры", ideol, enemy, FractionAggression.ambigious, false));

            ideol.Clear();
            enemy.Clear();

        }

        public static Fraction SearchFractionName(string name)
        {
            Fraction sel_fraction = AllFractions[0];
            foreach (Fraction fraction in AllFractions)
            {
                if (fraction.name == name)
                    sel_fraction = fraction;
            }
            return sel_fraction;
        }
    }
}
