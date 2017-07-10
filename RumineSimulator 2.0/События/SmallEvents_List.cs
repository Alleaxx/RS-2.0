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

        public static SmallEvent CommentWrite()
        {
            //Создание события
            User user = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            SmallEvent Event = new SmallEvent($"Комментарий от {user}", EventType.comment);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick));
            Event.participants.Add(user, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.EventAdd3_Mods(random.Next(2), 0, 0, 0);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            user.comments++;
            int likes = 0;
            Activity.curr_day_comments++;
            if (AdvRnd.PersentChanseBool(user.forum_influence / 10))
            {
                likes = random.Next(3);
                user.likes = user.comments_rate + likes;
            }
            //Информация в интерфейсе
            Event.eventSpec_properties.Add(new GuiString("Информация о комментарии: ", "", false, StringProfile.Header));
            Event.eventSpec_properties.Add(new GuiString("Плюсы комментария: ", likes.ToString(), true));
            Event.eventSpec_properties.Add(new GuiString($"Влияние:  {user.forum_influence}, шанс: ", (user.forum_influence / 10).ToString(), true));
            return Event;
        }
        public static SmallEvent NewsWrite()
        {
            //Создание события
            User user = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            SmallEvent Event = new SmallEvent($"Новость от {user}", EventType.news);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user.nick));
            Event.participants.Add(user, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.EventAdd3_Mods(random.Next(3), 2, 1, 0);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            user.news++;
            Activity.curr_day_news++;
            int quality = random.Next(user.news_quality - 5, user.news_quality + 5);
            //Информация в интерфейсе
            Event.eventSpec_properties.Add(new GuiString("Информация о новости: ", "", false, StringProfile.Header));
            Event.eventSpec_properties.Add(new GuiString("Качество новости: ", quality.ToString(), true));
            return Event;
        }

        public static SmallEvent ReputationChange_Random()
        {
            //Создание события
            User user_init = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            User user_goal = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            if (user_init == user_goal)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"Изменение репутации {user_goal} пользователем {user_init}", EventType.reputation);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, user_init.nick)
                );
            Event.participants.Add(user_init, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.participants.Add(user_goal, "Задумчиво смотрел на свою репутацию");
            Event.EventAdd3_Mods(0, 0, 0, 0);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_repChanges++;
            float karma_result = 0;
            string change_reason = "";
            if (user_init.relations.RelationStateReturn(user_goal) == RelationType.comrade ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.friend)
            {
                karma_result = user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(false);
                user_goal.reputation.ChangeReputation(user_init, user_init.karma.karma, change_reason);
            }
            else if (user_init.relations.RelationStateReturn(user_goal) == RelationType.unfriend ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.enemy)
            {
                karma_result = -user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(true);
                user_goal.reputation.ChangeReputation(user_init, -user_init.karma.karma, change_reason);
            }
            else
            {
                if (AdvRnd.PersentChanseBool(75))
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
            Event.eventSpec_properties.Add(new GuiString("Информация о репутации: ", "", false, StringProfile.Header));
            Event.eventSpec_properties.Add(new GuiString("Кто поставил: ", user_init.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Кому поставили: ", user_goal.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Проставленная карма: ", karma_result.ToString(), true));
            return Event;
        }
        public static SmallEvent ReputationChange_Reason(Event reason)
        {
            //Создание события
            User user_init = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            User user_goal = UsersControl.UserSearch(reason.participants.ElementAt(0).Key.nick);
            if (user_init == user_goal)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"Изменение репутации {user_goal} пользователем {user_init}", EventType.reputation);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Event, reason.Name)
                );
            Event.participants.Add(user_init, Roles_Creator[random.Next(Roles_Creator.Count)]);
            Event.participants.Add(user_goal, "Задумчиво смотрел на свою репутацию");
            Event.EventAdd3_Mods(0, 0, 0, 0);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_repChanges++;
            float karma_result = 0;
            string change_reason = "";
            if (user_init.relations.RelationStateReturn(user_goal) == RelationType.comrade ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.friend)
            {
                karma_result = user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(false);
                user_goal.reputation.ChangeReputation(user_init, user_init.karma.karma,change_reason);
            }
            else if (user_init.relations.RelationStateReturn(user_goal) == RelationType.unfriend ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.enemy)
            {
                karma_result = -user_init.karma.karma;
                change_reason = ReputationReason.ReturnReason(true);
                user_goal.reputation.ChangeReputation(user_init, -user_init.karma.karma, change_reason);
            }
            else
            {
                if (AdvRnd.PersentChanseBool(75))
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
            Event.eventSpec_properties.Add(new GuiString("Информация о репутации: ", "", false, StringProfile.Header));
            Event.eventSpec_properties.Add(new GuiString("Кто поставил: ", user_init.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Кому поставили: ", user_goal.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Причина: ", reason.Name, true));
            Event.eventSpec_properties.Add(new GuiString("Проставленная карма: ", karma_result.ToString(), true));

            return Event;
        }

        public static SmallEvent Ban_Random()
        {

            //Создание события
            List<User> moders = new List<User>();
            foreach (User moder in UsersControl.Users)
            {
                if (moder.mod)
                    moders.Add(moder);
            }
            User user_init = moders[random.Next(moders.Count)];
            User user_goal = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            if (user_init == user_goal)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"{user_init} несет Закон и Порядок ", EventType.ban);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User,
                user_init.nick));
            Event.participants.Add(user_init, Roles_moderator[random.Next(Roles_moderator.Count)]);
            Event.participants.Add(user_goal, Roles_moderator_victim[random.Next(Roles_moderator_victim.Count)]);
            Event.EventAdd3_Mods(random.Next(1, 4), 1, 0, 0);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_bans++;
            bool banned = false;
            int chanse = BanCheck(user_init, user_goal);
            //Информация в интерфейсе
            Event.eventSpec_properties.Add(new GuiString("Информация о бане: ", "", false, StringProfile.Header));
            Event.eventSpec_properties.Add(new GuiString("Модератор: ", user_init.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Жертва: ", user_goal.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Забанен?: ", banned.ToString(), true));
            Event.eventSpec_properties.Add(new GuiString("Шанс бана: ", chanse.ToString(), true));
            return Event;
        }
        public static SmallEvent Ban_Reason(Event reason)
        {

            //Создание события
            List<User> moders = new List<User>();
            foreach (User moder in UsersControl.Users)
            {
                if (moder.mod)
                    moders.Add(moder);
            }
            User user_init = moders[random.Next(moders.Count)];
            User user_goal = UsersControl.UserSearch(reason.participants.ElementAt(0).Key.nick);
            if (user_init == user_goal || reason.participants.Count == 0)
            {
                return FailEvent();
            }
            SmallEvent Event = new SmallEvent($"{user_init} несет Закон и Порядок ", EventType.ban);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Event, reason.Name));
            Event.participants.Add(user_init, Roles_moderator[random.Next(Roles_moderator.Count)]);
            Event.participants.Add(user_goal, Roles_moderator_victim[random.Next(Roles_moderator_victim.Count)]);
            Event.EventAdd3_Mods(random.Next(1, 4), 1, 0, 0);
            Event.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_bans++;
            bool banned = false;
            int chanse = BanCheck(user_init, user_goal);
            //Приговор
            if (AdvRnd.PersentChanseBool(chanse))
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
            Event.eventSpec_properties.Add(new GuiString("Информация о бане: ", "", false, StringProfile.Header));
            Event.eventSpec_properties.Add(new GuiString("Модератор: ", user_init.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Жертва: ", user_goal.nick, true));
            Event.eventSpec_properties.Add(new GuiString("Причина: ", reason.Name, true));
            Event.eventSpec_properties.Add(new GuiString("Забанен?: ", banned.ToString(), true));
            Event.eventSpec_properties.Add(new GuiString("Шанс бана: ", chanse.ToString(), true));
            return Event;
        }
        static int BanCheck(User user_init,User user_goal)
        {
            int chanse = -10;
            //Расчет шансов
            if (user_init.relations.RelationStateReturn(user_goal) == RelationType.comrade ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.friend)
            {
                chanse -= 20;
            }
            else if (user_init.relations.RelationStateReturn(user_goal) == RelationType.unfriend ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.enemy)
            {
                chanse += 20;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.rak)))
            {
                chanse += 75;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.madguy)))
            {
                chanse += 40;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.accurateguy)))
            {
                chanse -= 10;
            }
            if (user_init.traits.Contains(TraitsList.SearchTrait(TraitsType.tiran)))
            {
                chanse += 60;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.ded)))
            {
                chanse -= 20;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.kindguy)))
            {
                chanse -= 10;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.srednefag)))
            {
                chanse -= 10;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.revolutioner)))
            {
                chanse += 25;
            }
            return chanse;
        }


        public static SmallEvent FailEvent()
        {
            SmallEvent Event = new SmallEvent("Фейл", EventType.fail);
            Event.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Event, "Мде"));
            Event.EventAdd3_Mods(0, 0, 0, 0);
            Event.EventAdd4_Participants(UsersControl.Users[random.Next(UsersControl.Users.Count)], Roles_moderator[random.Next(Roles_moderator.Count)]);
            Event.EventAdd6_Dates(0);
            return Event;
        }
    }
}
