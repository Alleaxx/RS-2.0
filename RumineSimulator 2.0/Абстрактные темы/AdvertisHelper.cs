using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    //Замена ключевых слов в строке на рандомные темы
    static class AdvertisHelper
    {
        // user заменяется на ник
        // date заменяется на дату
        // topic заменяется на тему
        // state заменяется на состояние
        // diskuss заменяется на тип дискуссии
        // wish заменяется на желание
         
        public static List<string> states = new List<string>() {"херово","прекрасно","счастливо","невероятно","в ударе","отвратно",
        "уверенно","стыдливо","мерзко","восхитительно","противно","паскудно","богомерзко","погано","отчаянно","удивительно",
        "поразительно"};
        public static List<string> diskuss = new List<string>() {"срач","спор","флуд","совет","диспут"};
        public static List<string> wishes = new List<string>() {"желает","хочет","надеется на","верит в","молится на","негодует от отсутствия",
        "жаждет","бредит о","рвется к","зарится на"};
        public static List<string> qualitys = new List<string>() {"хороший","плохой","не очень","трушный","прекрасный","отличный",
        "превосходный","восхитительный","крутой","мачо"};

        //Замена текста с учетом события
        public static string TextRandom(string t, Event eventt)
        {
            t = t.Replace("user_nick", $"{eventt.Creator.Text}");
            t = t.Replace("date", $"{eventt.date.ToLongDateString()} в {eventt.date.ToShortTimeString()}");
            string random_state = states[AdvRnd.random.Next(states.Count)];
            t = t.Replace("state", random_state);
            string random_diskuss = diskuss[AdvRnd.random.Next(diskuss.Count)];
            t = t.Replace("diskuss", random_diskuss);
            string random_wish = wishes[AdvRnd.random.Next(wishes.Count)];
            t = t.Replace("wish", random_wish);
            string random_qual = qualitys[AdvRnd.random.Next(qualitys.Count)];
            t = t.Replace("quality", random_qual);
            return t;
        }
        public static string TextRandom(string t, User user)
        {
            t = t.Replace("user_nick", $"{user.nick}");
            return t;
        }

        //Замена текста 
        public static string TextRandom(string t)
        {
            string random_state = states[AdvRnd.random.Next(states.Count)];
            t = t.Replace("state", random_state);
            string random_diskuss = diskuss[AdvRnd.random.Next(diskuss.Count)];
            t = t.Replace("diskuss", random_diskuss);
            string random_wish = wishes[AdvRnd.random.Next(wishes.Count)];
            t = t.Replace("wish", random_wish);
            string random_qual = qualitys[AdvRnd.random.Next(qualitys.Count)];
            t = t.Replace("quality", random_qual);
            return t;
        }
    }
    enum Abbrev
    {
        user,date,topic,state,diskuss,wish,quality
    }
}
