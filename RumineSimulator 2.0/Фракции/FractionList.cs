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
            List<TraitsType> ideol = new List<TraitsType>();
            List<TraitsType> enemy = new List<TraitsType>();
            NeutralFraction = new Fraction("Нейтралы", ideol, enemy, FractionAggression.peacefull,false, true);

            ideol = new List<TraitsType>() {TraitsType.commie};
            enemy = new List<TraitsType>() { TraitsType.fashik};
            AllFractions.Add(new Fraction("Верующие коммунисты",ideol,enemy,FractionAggression.light,false));

            ideol = new List<TraitsType>() { };
            enemy = new List<TraitsType>() { TraitsType.rak };
            AllFractions.Add(new Fraction("Общество раковых гурманов", ideol, enemy, FractionAggression.aggressive, false));

            ideol = new List<TraitsType>() { TraitsType.rak };
            enemy = new List<TraitsType>() { TraitsType.inteligent };
            AllFractions.Add(new Fraction("1-е Румынские Народные Освободители", ideol, enemy, FractionAggression.aggressive, false));

            ideol = new List<TraitsType>() { TraitsType.nazi };
            enemy = new List<TraitsType>() { TraitsType.newfag};
            AllFractions.Add(new Fraction("Форумные нацисты", ideol, enemy, FractionAggression.aggressive, false));

            ideol = new List<TraitsType>() { TraitsType.fashik };
            enemy = new List<TraitsType>() { TraitsType.commie };
            AllFractions.Add(new Fraction("Верующие фашисты", ideol, enemy, FractionAggression.ambigious,false));

            ideol = new List<TraitsType>() { TraitsType.ded,TraitsType.natural };
            enemy = new List<TraitsType>() { TraitsType.nevecherya };
            AllFractions.Add(new Fraction("Совет дедов-натуралов", ideol, enemy, FractionAggression.normal,true));

            ideol = new List<TraitsType>() { TraitsType.programmer, TraitsType.arter,TraitsType.writer };
            enemy = new List<TraitsType>() { TraitsType.rak };
            AllFractions.Add(new Fraction("MicroShit", ideol, enemy, FractionAggression.light,false));

            ideol = new List<TraitsType>() { TraitsType.pane};
            enemy = new List<TraitsType>() {};
            AllFractions.Add(new Fraction("Броняши", ideol, enemy, FractionAggression.ambigious, false));

            ideol = new List<TraitsType>() {};
            enemy = new List<TraitsType>() { TraitsType.pane };
            AllFractions.Add(new Fraction("Хейтеры", ideol, enemy, FractionAggression.ambigious, false));

            ideol.Clear();
            enemy.Clear();

        }

        //Поиск фракции по параметрам
        public static Fraction SearchFraction(string name)
        {
            Fraction sel_fraction = AllFractions[0];
            foreach (Fraction fraction in AllFractions)
            {
                if (fraction.name == name)
                    sel_fraction = fraction;
            }
            return sel_fraction;
        }
        public static Fraction SearchFraction(int id)
        {
            Fraction sel_fraction = AllFractions[0];
            foreach (Fraction fraction in AllFractions)
            {
                if (fraction.id == id)
                    sel_fraction = fraction;
            }
            return sel_fraction;
        }
    }
}
