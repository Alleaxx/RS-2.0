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
        static List<string> Roles_Creator = new List<string> {"Изобретал и ваял","Думал чего бы поделать","Внёс свою лепту в развитие сайта",
        "Находился в поиске пути","Валял дурака"};
        static List<string> Roles_moderator = new List<string> {"За справедливость!","Я на страже порядка","Нарушители понесут наказание",
        "Кто  не согласен - тому бан","Мой банхаммер наготове"};
        static List<string> Roles_moderator_victim = new List<string> {"Я хотел сделать как лучше!","Они ничего не смыслят в румине","Я делаю то, что должно",
        "Наше дело правое, победа будет за нами","Голос правды не угаснет никогда"};

        public static SmallEvent MessageWrite()
        {
            //Создание события
            User user = UserList.Users[random.Next(UserList.Users.Count)];
            SmallEvent Event = new SmallEvent($"Сообщение от {user}", EventType.message);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick), "Некто написал сообщение на форуме. Ничего необычного");
            Event.participants.Add(user, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.EventAdd3_Mods(random.Next(3), 1, 0, 0,1);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            user.messages++;
            Activity.curr_day_messages++;
            int likes = 0;
            int chanse = 0;
            chanse = user.forum_influence / 5;
            if (AdvRandom.PersentChanseBool(chanse))
            {
                likes = random.Next(3);
                user.likes = user.likes + likes;
            }
            //Информация в интерфейсе
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Информация о сообщении: ", "", false,true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Симпатии к сообщению: ", likes.ToString(), true));
            Event.InterfaceInfo.Add_Property(new Interface_String($"Влияние:  {user.forum_influence}, шанс: ", chanse.ToString(), true));
            return Event;
        }
        public static SmallEvent CommentWrite()
        {
            //Создание события
            User user = UserList.Users[random.Next(UserList.Users.Count)];
            SmallEvent Event = new SmallEvent($"Комментарий от {user}", EventType.comment);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick), "Какой-то пользователь прокомментировал новость");
            Event.participants.Add(user, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.EventAdd3_Mods(random.Next(2), 0, 0, 0,1);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            user.comments++;
            int likes = 0;
            Activity.curr_day_comments++;
            if (AdvRandom.PersentChanseBool(user.forum_influence / 10))
            {
                likes = random.Next(3);
                user.likes = user.comments_rate + likes;
            }
            //Информация в интерфейсе
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Информация о комментарии: ", "", false, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Плюсы комментария: ", likes.ToString(), true));
            Event.InterfaceInfo.Add_Property(new Interface_String($"Влияние:  {user.forum_influence}, шанс: ", (user.forum_influence / 10).ToString(), true));
            return Event;
        }
        public static SmallEvent NewsWrite()
        {
            //Создание события
            User user = UserList.Users[random.Next(UserList.Users.Count)];
            SmallEvent Event = new SmallEvent($"Новость от {user}", EventType.news);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick), "Какой-то пользователь создал новость на румине");
            Event.participants.Add(user, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.EventAdd3_Mods(random.Next(3), 2, 1, 0,50);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            user.news++;
            Activity.curr_day_news++;
            int quality = random.Next(user.news_quality - 5, user.news_quality + 5);
            //Информация в интерфейсе
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Информация о новости: ", "", false, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Качество новости: ", quality.ToString(), true));
            return Event;
        }

        public static SmallEvent ReputationChange_Random()
        {
            //Создание события
            User user_init = UserList.Users[random.Next(UserList.Users.Count)];
            User user_goal = UserList.Users[random.Next(UserList.Users.Count)];
            if (user_init == user_goal)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"Изменение репутации {user_goal} пользователем {user_init}", EventType.reputation);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user_init.nick),
                "Пользователь изменил при помощи своей кармы репутацию другому юзеру",false);
            Event.participants.Add(user_init, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.participants.Add(user_goal, "Задумчиво смотрел на свою репутацию");
            Event.EventAdd3_Mods(0, 0, 0, 0,20);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_repChanges++;
            float karma_result = 0;
            string change_reason = "";
            if (user_init.relations.All[user_goal].relation == RelationsEnum.comrade ||
                user_init.relations.All[user_goal].relation == RelationsEnum.friend)
            {
                karma_result = user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(false);
                user_goal.reputation.ChangeReputation(user_init, user_init.karma.karma, change_reason);
            }
            else if (user_init.relations.All[user_goal].relation == RelationsEnum.unfriend ||
                user_init.relations.All[user_goal].relation == RelationsEnum.enemy)
            {
                karma_result = -user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(true);
                user_goal.reputation.ChangeReputation(user_init, -user_init.karma.karma, change_reason);
            }
            else
            {
                if (AdvRandom.PersentChanseBool(75))
                {
                    karma_result = user_init.karma.karma;
                    change_reason = ReputationReason.ReturnReason(false);
                    user_goal.reputation.ChangeReputation(user_init, user_init.karma.karma, change_reason);

                }
                else
                {
                    karma_result = -user_init.karma.karma;
                    change_reason = ReputationReason.ReturnReason(true);
                    user_goal.reputation.ChangeReputation(user_init, -user_init.karma.karma, change_reason);
                }
            }
            Event.participants[user_init] = change_reason;
            //Информация в интерфейсе
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Информация о репутации: ", "", false, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Кто поставил: ", user_init.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Кому поставили: ", user_goal.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Проставленная карма: ", karma_result.ToString(), true));
            return Event;
        }
        public static SmallEvent ReputationChange_Reason(Event reason)
        {
            //Создание события
            User user_init = UserList.Users[random.Next(UserList.Users.Count)];
            User user_goal = UserList.UserSearch(reason.participants.ElementAt(0).Key.nick);
            if (user_init == user_goal)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"Изменение репутации {user_goal} пользователем {user_init}", EventType.reputation);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Event, reason.Name),
                $"Пользователь в ответ на действие {user_goal} проставил ему репутацию",false);
            Event.participants.Add(user_init, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.participants.Add(user_goal, "Задумчиво смотрел на свою репутацию");
            Event.EventAdd3_Mods(0, 0, 0, 0,20);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_repChanges++;
            float karma_result = 0;
            string change_reason = "";
            if (user_init.relations.All[user_goal].relation == RelationsEnum.comrade ||
                user_init.relations.All[user_goal].relation == RelationsEnum.friend)
            {
                karma_result = user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(false);
                user_goal.reputation.ChangeReputation(user_init, user_init.karma.karma,change_reason);
            }
            else if (user_init.relations.All[user_goal].relation == RelationsEnum.unfriend ||
                user_init.relations.All[user_goal].relation == RelationsEnum.enemy)
            {
                karma_result = -user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(true);
                user_goal.reputation.ChangeReputation(user_init, -user_init.karma.karma, change_reason);
            }
            else
            {
                if (AdvRandom.PersentChanseBool(75))
                {
                    karma_result = user_init.karma.karma;
                    change_reason = ReputationReason.ReturnReason(false);
                    user_goal.reputation.ChangeReputation(user_init, user_init.karma.karma, change_reason);

                }
                else
                {
                    karma_result = -user_init.karma.karma;
                    change_reason = ReputationReason.ReturnReason(true);
                    user_goal.reputation.ChangeReputation(user_init, -user_init.karma.karma, change_reason);
                }
            }
            Event.participants[user_init] = change_reason;
            reason.connected_events.Add(Event);
            Event.connected_events.Add(reason);
            //Информация в интерфейсе
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Информация о репутации: ", "", false, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Кто поставил: ", user_init.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Кому поставили: ", user_goal.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Причина: ", reason.Name, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Проставленная карма: ", karma_result.ToString(), true));

            return Event;
        }

        public static SmallEvent Ban_Random()
        {

            //Создание события
            List<User> moders = new List<User>();
            foreach (User moder in UserList.Users)
            {
                if (moder.mod)
                    moders.Add(moder);
            }
            User user_init = moders[random.Next(moders.Count)];
            User user_goal = UserList.Users[random.Next(UserList.Users.Count)];
            if (user_init == user_goal)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"{user_init} несет Закон и Порядок ", EventType.ban);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User,
                user_init.nick), $"Модератор {user_init.nick} заметил неподобающее поведение одного из участников форума и пытается его пресечь!");
            Event.participants.Add(user_init, Roles_moderator[random.Next(Roles_moderator.Count)]);
            Event.participants.Add(user_goal, Roles_moderator_victim[random.Next(Roles_moderator_victim.Count)]);
            Event.EventAdd3_Mods(random.Next(1, 4), 1, 0, 0,200);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_bans++;
            bool banned = false;
            int chanse = BanCheck(user_init, user_goal);
            //Информация в интерфейсе
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Информация о бане: ", "", false, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Модератор: ", user_init.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Жертва: ", user_goal.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Забанен?: ", banned.ToString(), true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Шанс бана: ", chanse.ToString(), true));
            return Event;
        }
        public static SmallEvent Ban_Reason(Event reason)
        {

            //Создание события
            List<User> moders = new List<User>();
            foreach (User moder in UserList.Users)
            {
                if (moder.mod)
                    moders.Add(moder);
            }
            User user_init = moders[random.Next(moders.Count)];
            User user_goal = UserList.UserSearch(reason.participants.ElementAt(0).Key.nick);
            if (user_init == user_goal)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"{user_init} несет Закон и Порядок ", EventType.ban);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Event, reason.Name), $"Модератор {user_init.nick} заметил {reason.Name}");
            Event.participants.Add(user_init, Roles_moderator[random.Next(Roles_moderator.Count)]);
            Event.participants.Add(user_goal, Roles_moderator_victim[random.Next(Roles_moderator_victim.Count)]);
            Event.EventAdd3_Mods(random.Next(1, 4), 1, 0, 0,200);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_bans++;
            bool banned = false;
            int chanse = BanCheck(user_init, user_goal);
            //Приговор
            if (AdvRandom.PersentChanseBool(chanse))
            {
                user_goal.LastBan.AddWarnings(user_init, 100, Event.Name, user_goal);
                banned = true;
            }
            else
            {
                banned = false;
            }
            reason.connected_events.Add(Event);
            Event.connected_events.Add(reason);
            //Информация в интерфейсе
            Event.EventAddEnd_InterfaceInfo();
            Event.InterfaceInfo.Add_Property(new Interface_String("Информация о бане: ", "", false, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Модератор: ", user_init.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Жертва: ", user_goal.nick, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Причина: ", reason.Name, true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Забанен?: ", banned.ToString(), true));
            Event.InterfaceInfo.Add_Property(new Interface_String("Шанс бана: ", chanse.ToString(), true));
            return Event;
        }
        static int BanCheck(User user_init,User user_goal)
        {
            int chanse = -10;
            //Расчет шансов
            if (user_init.relations.All[user_goal].relation == RelationsEnum.comrade ||
                user_init.relations.All[user_goal].relation == RelationsEnum.friend)
            {
                chanse -= 20;
            }
            else if (user_init.relations.All[user_goal].relation == RelationsEnum.unfriend ||
                user_init.relations.All[user_goal].relation == RelationsEnum.enemy)
            {
                chanse += 20;
            }
            if (user_goal.traits.Contains(TraitsList.AllTraits[Traits.rak]))
            {
                chanse += 75;
            }
            if (user_goal.traits.Contains(TraitsList.AllTraits[Traits.madguy]))
            {
                chanse += 40;
            }
            if (user_goal.traits.Contains(TraitsList.AllTraits[Traits.accurateguy]))
            {
                chanse -= 10;
            }
            if (user_init.traits.Contains(TraitsList.AllTraits[Traits.tiran]))
            {
                chanse += 60;
            }
            if (user_goal.traits.Contains(TraitsList.AllTraits[Traits.ded]))
            {
                chanse -= 20;
            }
            if (user_goal.traits.Contains(TraitsList.AllTraits[Traits.kindguy]))
            {
                chanse -= 10;
            }
            if (user_goal.traits.Contains(TraitsList.AllTraits[Traits.srednefag]))
            {
                chanse -= 10;
            }
            if (user_goal.traits.Contains(TraitsList.AllTraits[Traits.revolutioner]))
            {
                chanse += 25;
            }
            return chanse;
        }


        public static SmallEvent FailEvent()
        {
            SmallEvent Event = new SmallEvent("Фейл", EventType.fail);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Event, "Мде"), "Этот пользователь хотел нам что-то сказать. Но что - мы так и не узнаем...");
            Event.EventAdd3_Mods(0, 0, 0, 0);
            Event.EventAdd4_Participants(UserList.Users[random.Next(UserList.Users.Count)], Roles_moderator[random.Next(Roles_moderator.Count)]);
            Event.EventAdd6_Dates(0);
            Event.EventAddEnd_InterfaceInfo();
            return Event;
        }
    }
}
