using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class AdvertisControl
    {
        //Вернуть случайное объявление на основе события
        public static string GetAdvertisEvent(Event Event)
        {
            if (Event.descriptions.Count != 0)
            {
                string s = Event.descriptions[AdvRnd.random.Next(Event.descriptions.Count)];
                s = AdvertisHelper.TextRandom(s,Event);
                if (Event.Creator.Type == CreatorType.User && AdvRnd.PrsChanse(1))
                    UsersControl.UserSearch(Event.Creator.Text).oldAdvertisments.Add(s);
                return s;
            }
            return "Слава руминю! Руминю слава!";
        }


    }
}
