using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class AdvertisHelper
    {
        // user заменяется на ник
        // date заменяется на дату
        // topic заменяется на тему
        // state заменяется на состояние
        // diskuss заменяется на тип дискуссии
        // wish заменяется на желание
         
        private static List<string> topics = new List<string>() {"линукс","игры","сайт","тлен","активность","группы",
        "ватафаки","модераторов"};
        private static List<string> states = new List<string>() {"херово","прекрасно","счастливо","невероятно","в ударе","отвратно",
        "уверенно","стыдливо","мерзко","восхитительно","противно","паскудно","богомерзко","погано","отчаянно","удивительно",
        "поразительно"};
        private static List<string> diskuss = new List<string>() {"срач","спор","флуд","мем","совет","диспут"};
        private static List<string> wishes = new List<string>() {"желает","хочет","надеется на","верит в","молится на","негодует от отсутствия",
        "жаждет","бредит о","рвется к","зарится на"};
        private static List<string> qualitys = new List<string>() {"хороший","плохой","не очень","трушный","прекрасный","отличный",
        "превосходный","восхитительный","крутой","мачо"};


        public static string TextRandom(string t, Event eventt)
        {
            t = t.Replace("user_nick", $"{eventt.Creator.Text}");
            t = t.Replace("date", $"{eventt.date.ToShortDateString()} {eventt.date.ToShortTimeString()}");
            string random_topic = topics[AdvRnd.random.Next(topics.Count)];
            t = t.Replace("topic", random_topic);
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
    }
    enum Abbrev
    {
        user,date,topic,state,diskuss,wish,quality
    }
}
