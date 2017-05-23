using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class SmallEvents_List
    {
        static public List<SmallEvent> SmallEvents = new List<SmallEvent>();
        static Random random = new Random();
        public static SmallEvent MessageWrite()
        {
            User user = UserList.Users[random.Next(UserList.Users.Count)];
            SmallEvent Event = new SmallEvent($"Сообщение от {user}",EventType.message);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick), "Некто написал сообщение на форуме. Ничего необычного");
            Event.EventAdd2_Mods(random.Next(3), 1, 0, 0);
            user.messages++;
            int likes = 0;
            if (AdvRandom.PersentChanseBool(user.forum_influence / 10))
            {
                likes = random.Next(3);
                user.likes = user.likes + likes;
            }
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Симпатии к сообщению", likes.ToString(), true));
            Event.InterfaceInfo.Add_Property(new Interface_String($"Влияние {user.forum_influence}, шанс", (user.forum_influence / 10).ToString(), true));
            return Event;
        }
        public static SmallEvent CommentWrite()
        {
            User user = UserList.Users[random.Next(UserList.Users.Count)];
            SmallEvent Event = new SmallEvent($"Комментарий от {user}", EventType.comment);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick), "Какой-то пользователь прокомментировал новость");
            Event.EventAdd2_Mods(random.Next(2), 0, 0, 0);
            user.comments++;
            int likes = 0;
            if (AdvRandom.PersentChanseBool(user.forum_influence / 10))
            {
                likes = random.Next(3);
                user.likes = user.comments_rate+ likes;
            }
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Плюсы комментария", likes.ToString(), true));
            Event.InterfaceInfo.Add_Property(new Interface_String($"Влияние {user.forum_influence}, шанс", (user.forum_influence / 10).ToString(), true));
            return Event;
        }
        public static SmallEvent NewsWrite()
        {
            User user = UserList.Users[random.Next(UserList.Users.Count)];
            SmallEvent Event = new SmallEvent($"Новость от {user}", EventType.news);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick), "Какой-то пользователь создал новость на румине");
            Event.EventAdd2_Mods(random.Next(3), 2, 1, 0);
            user.news++;
            int quality = random.Next(user.news_quality - 5, user.news_quality + 5);
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Качество новости", quality.ToString(), true));
            return Event;
        }
    }
}
