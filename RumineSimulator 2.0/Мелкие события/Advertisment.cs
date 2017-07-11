using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Advertisment
    {
        // user_nick заменяется на ник


        //Вернуть случайное объявление на основе трейта какого-то пользователя
        public static string GetAdvertisTrait(User user,Trait trait)
        {
            if(trait.Advertisments.Count != 0)
            {
                string s = trait.Advertisments[AdvRnd.random.Next(trait.Advertisments.Count)];
                s.Replace("user_nick", $"{user.nick}");
                user.oldAdvertisments.Add(s);
                return s;
            }
            return "Слава руминю! Руминю слава!";
        }
        public static string GetAdvertisCharFeature(User user, CharFeature feature)
        {
            if (feature.Advertisments.Count != 0)
            {
                string s = feature.Advertisments[AdvRnd.random.Next(feature.Advertisments.Count)];
                s.Replace("user_nick", $"{user.nick}");
                user.oldAdvertisments.Add(s);
                return s;
            }
            return "Слава руминю! Руминю слава!";
        }
        public static string GetAdvertisGroup(User user, Group group)
        {
            if (group.Advertisments.Count != 0)
            {
                string s = group.Advertisments[AdvRnd.random.Next(group.Advertisments.Count)];
                s.Replace("user_nick", $"{user.nick}");
                user.oldAdvertisments.Add(s);
                return s;
            }
            return "Слава руминю! Руминю слава!";
        }
        public static string GetAdvertisEventStat(EventStatChange Event)
        {
            if (Event.Advertisments.Count != 0)
            {
                string s = Event.Advertisments[AdvRnd.random.Next(Event.Advertisments.Count)];
                if(Event.Creator.Type == CreatorType.User && AdvRnd.PrsChanse(1))
                    UsersControl.UserSearch(Event.Creator.Text).oldAdvertisments.Add(s);
                return s;
            }
            return "Слава руминю! Руминю слава!";
        }


    }
    enum AdvertisType
    {
        trait, charFeature
    }
}
