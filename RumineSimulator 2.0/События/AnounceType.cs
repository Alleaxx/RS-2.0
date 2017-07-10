using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class AnounceType
    {
        static Dictionary<TraitsType, List<string>> Dictionary = new Dictionary<TraitsType, List<string>>();
        static List<string> notrait = new List<string>();
        static List<string> Succes = new List<string>();
        static List<string> Fail = new List<string>();
        public static void AnounceInit()
        {
            notrait.Add("Все ньюфаги должны страдать!");
            notrait.Add("Игры не нужны!");
            notrait.Add("Модеров на кол!");
            notrait.Add("Олдфаги - тупое отребье!");
            notrait.Add("Нужна революция!");
            notrait.Add("Больше групп!");
            notrait.Add("Линукс для калоедов!");
            Dictionary.Add(TraitsType.notrait, notrait);
        }
        public static string ReturnAnounce(TraitsType trait)
        {
            List<string> poss = new List<string>();
            for (int i = 0; i < Dictionary.Count; i++)
            {
                if (Dictionary.ElementAt(i).Key == trait)
                    poss = Dictionary.ElementAt(i).Value;
            }
            if (poss.Count == 0)
            {
                for (int i = 0; i < Dictionary.Count; i++)
                {
                    if (Dictionary.ElementAt(i).Key == TraitsType.notrait)
                        poss = Dictionary.ElementAt(i).Value;
                }
            }
            return poss[AdvRnd.random.Next(poss.Count)];
        }
        public static string ReturnResult(bool result)
        {
            if (result)
            {
                return Succes[AdvRnd.random.Next(Succes.Count)];
            }
            else
            {
                return Fail[AdvRnd.random.Next(Fail.Count)];
            }
        }
    }
}
